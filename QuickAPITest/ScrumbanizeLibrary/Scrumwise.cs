using QuickAPITest.Definitions;
using RestSharp;
using RestSharp.Authenticators;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace QuickAPITest
{

	public class Scrumwise
	{

		private string _apiurl;
		private string _userName;
		private string _key;
        private string _kanbanizeTagID;
        private string _projectID;
        private string _backlogListID;
        private string _templateTagID;
        private string _teamID;
        private string _estimateUnit;

        public Scrumwise(string scrumwiseProjectID, string scrumwiseUser, string scrumwiseKey, string scrumwiseAPI, string scrumwiseBacklogListID, string scrumwiseKanbanizeTag)
        {
            this._userName = scrumwiseUser;
            this._key = scrumwiseKey;
            this._apiurl = scrumwiseAPI;
            this._kanbanizeTagID = scrumwiseKanbanizeTag;
            this._projectID = scrumwiseProjectID;
            this._backlogListID = scrumwiseBacklogListID;
        }

        public Scrumwise(string scrumwiseProjectID, string scrumwiseUser, string scrumwiseKey, string scrumwiseAPI, string scrumwiseBacklogListID, string scrumwiseTemplateTagID, string scrumwiseTeamID, string scrumwiseEstimateUnit)
        {
            this._userName = scrumwiseUser;
            this._key = scrumwiseKey;
            this._apiurl = scrumwiseAPI;
            this._projectID = scrumwiseProjectID;
            this._backlogListID = scrumwiseBacklogListID;
            this._templateTagID = scrumwiseTemplateTagID;
            this._teamID = scrumwiseTeamID;
            this._estimateUnit = scrumwiseEstimateUnit;
        }


        public bool ImportKanbanizeToScrumwise(ScrumwiseItemList kanbanizeTaskList, ScrumwiseItemList scrumwiseItemList)
		{
			foreach (Backlogitem kanbanTask in kanbanizeTaskList.TaskList)
			{

				if (!scrumwiseItemList.TaskList.Exists(x => x.externalID.Equals(kanbanTask.externalID))) // If the Kanbanize task already exists in Scrumwise, don't try to create it again
					if (!CreateBacklogItem(kanbanTask))
					{
						return false;
					}
			}
			return true;
		}

        public bool CreateSprintTemplate(Sprint sprintToBeTemplated, ScrumwiseItemList scrumwiseItemList )
        {
            foreach(Backlogitem backlogitem in scrumwiseItemList.TaskList)
            {
                backlogitem.sprintID = sprintToBeTemplated.id;
                for(int i=0; i< backlogitem.tagIDs.Count(); i++)
                {
                    if (backlogitem.tagIDs[i] == _templateTagID)
                    {
                        backlogitem.tagIDs[i] = null;
                    }
                }
                CreateBacklogItem(backlogitem);
            }
            return true;
        }

		public bool CreateBacklogItem(Backlogitem scrumwiseItem)
		{
			try
			{

				RestClient client = new RestClient(_apiurl);
				client.Authenticator = new HttpBasicAuthenticator(_userName, _key);
				RestRequest req = new RestRequest("addBacklogItem", Method.POST);

				req.AddParameter("projectID", scrumwiseItem.projectID);
				req.AddParameter("backlogListID", scrumwiseItem.backlogListID);
				req.AddParameter("externalID", scrumwiseItem.externalID);
				req.AddParameter("type", scrumwiseItem.type);
                //req.AddParameter("estimate", scrumwiseItem.estimate);
				//IMPORTANT FIX! Tell Brian ASAP
				if (_estimateUnit != null)
				{
					req.AddParameter("estimate", scrumwiseItem.estimate);
					req.AddParameter("estimateUnit", _estimateUnit);
				}
                req.AddParameter("name", scrumwiseItem.name);
                req.AddParameter("description", scrumwiseItem.description);

				if (scrumwiseItem.priority == ScrumwisePriority.Low.ToString())
				{
					req.AddParameter("priority", "Low");
				}
				else if (scrumwiseItem.priority == ScrumwisePriority.Medium.ToString())
				{
					req.AddParameter("priority", "Medium");
				}
				else if (scrumwiseItem.priority == ScrumwisePriority.High.ToString())
				{
					req.AddParameter("priority", "High");
				}
				else if (scrumwiseItem.priority == ScrumwisePriority.Urgent.ToString())
				{
					req.AddParameter("priority", "Urgent");
				}/*
				else
				{
					throw new Exception("Unknown priority");
				}*/


				var createResult = client.Execute<CreateBacklogItemResult>(req);
				if (createResult.IsSuccessful)
				{
					string itemID = createResult.Data.Result;
					scrumwiseItem.id = itemID;
					foreach (var tagID in scrumwiseItem.tagIDs)
					{
						AddTag(itemID, tagID);
					}
                    if (scrumwiseItem.sprintID!=null)
                    {
                        AddSprint(scrumwiseItem);
                    }

					return true;
				}
			}
			catch (Exception)
			{

				throw;
			}

			return false;
		}
		public bool setBacklogItemExternalID(Backlogitem scrumwiseItem)
		{
			try
			{

				RestClient client = new RestClient(_apiurl);
				client.Authenticator = new HttpBasicAuthenticator(_userName, _key);
				RestRequest req = new RestRequest("setBacklogItemExternalID", Method.POST);
				req.AddParameter("backlogItemID", scrumwiseItem.id);
				req.AddParameter("externalID", scrumwiseItem.externalID);



				var createResult = client.Execute<CreateBacklogItemResult>(req);

				return true;

			}
			catch (Exception)
			{
				return false;
				throw;
			}
		}
		public ScrumwiseItemList GetKanbanizeItemsInScrumwise()
		{
			ScrumwiseItemList scrumwiseItemList = new ScrumwiseItemList();
			scrumwiseItemList.TaskList = new List<Backlogitem>();
			Rootobject test = GetScrumwiseItems();
			foreach (Backlogitem backlogitem in test.result.projects[0].backlogItems)
			{
				if (backlogitem.tagIDs.Contains(_kanbanizeTagID))
				{
					scrumwiseItemList.TaskList.Add(backlogitem);
					if (backlogitem.externalID == null)
					{
						backlogitem.externalID = "0";
					}
				}
			}

			return scrumwiseItemList;
		}
		public Rootobject GetScrumwiseItems()
		{
			RestClient client = new RestClient(_apiurl);
			client.Authenticator = new HttpBasicAuthenticator(_userName, _key);
			RestRequest request = new RestRequest("getData", Method.POST);
			request.AddParameter("projectIDs", _projectID);
			request.AddParameter("includeProperties", "Project.backlogItems,BacklogItem.tasks");
			var response = client.Post(request);
			return SimpleJson.DeserializeObject<Rootobject>(response.Content);
		}

		private void AddTag(string itemID, string tagID)
		{
			RestClient client = new RestClient(_apiurl);
			client.Authenticator = new HttpBasicAuthenticator(_userName, _key);
			RestRequest request = new RestRequest("addTagOnObject", Method.POST);
			request.AddParameter("tagID", tagID);
			request.AddParameter("objectType", "BacklogItem");
			request.AddParameter("objectID", itemID);
			client.Execute(request);
		}
	

        public ScrumwiseItemList GetListItemsInScrumwise()
        {
            ScrumwiseItemList scrumwiseItemList = new ScrumwiseItemList();
            scrumwiseItemList.TaskList = new List<Backlogitem>();
            Rootobject test = GetScrumwiseItems();
            foreach (Backlogitem backlogitem in test.result.projects[0].backlogItems)
            {
                if (backlogitem.backlogListID.Equals(_backlogListID))
                {
                    scrumwiseItemList.TaskList.Add(backlogitem);
                }
            }

            return scrumwiseItemList;
        }

        public ScrumwiseItemList GetTemplateItemsInScrumwise()
        {
            ScrumwiseItemList scrumwiseItemList = GetListItemsInScrumwise();
            ScrumwiseItemList templateItemList = new ScrumwiseItemList();

            templateItemList.TaskList = new List<Backlogitem>();
            
            foreach (Backlogitem backlogitem in scrumwiseItemList.TaskList)
            {
                if (backlogitem.tagIDs.Contains(_templateTagID))
                {
                    templateItemList.TaskList.Add(backlogitem);
                }
            }

            return templateItemList;
        }

        public Sprint GetSprintInScrumwise(string sprintName)
        {
            Sprint foundSprint = new Sprint();
            Rootobject test = GetScrumwiseSprints();
            foreach (Sprint sprint in test.result.projects[0].sprints)
            {
                if (sprint.name.Equals(sprintName))
                {
                    foundSprint = sprint;
                }
            }

            return foundSprint;
        }
        public Rootobject GetScrumwiseSprints()
        {
            RestClient client = new RestClient(_apiurl);
            client.Authenticator = new HttpBasicAuthenticator(_userName, _key);
            RestRequest request = new RestRequest("getData", Method.POST);
            request.AddParameter("projectIDs", _projectID);
            request.AddParameter("includeProperties", "Project.sprints");
            var response = client.Post(request);
            return SimpleJson.DeserializeObject<Rootobject>(response.Content);
        }
        private void AddSprint(Backlogitem backlogitem)
        {
            RestClient client = new RestClient(_apiurl);
            client.Authenticator = new HttpBasicAuthenticator(_userName, _key);
            RestRequest request = new RestRequest("assignBacklogItemToSprint", Method.POST);
            request.AddParameter("sprintID", backlogitem.sprintID);
            request.AddParameter("teamID", _teamID);
            request.AddParameter("backlogItemID", backlogitem.id);
            var response = client.Execute(request);
        }
    }
}

