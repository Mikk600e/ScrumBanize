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

        public Scrumwise(string scrumwiseProjectID, string scrumwiseUser, string scrumwiseKey, string scrumwiseAPI, string scrumwiseKanbanizeTag) 
        {
            this._userName = scrumwiseUser;
            this._key = scrumwiseKey;
            this._apiurl = scrumwiseAPI;
            this._kanbanizeTagID = scrumwiseKanbanizeTag;
            this._projectID = scrumwiseProjectID;
        }

        public bool ImportKanbanizeToScrumwise(ScrumwiseItemList kanbanizeTaskList, ScrumwiseItemList scrumwiseItemList)
        {
            foreach (Backlogitem kanbanTask in kanbanizeTaskList.TaskList)
            {
                if (!scrumwiseItemList.TaskList.Exists(x => x.externalID.Equals(kanbanTask.externalID))) // If the Kanbanize task already exists in Scrumwise, don't try to create it again
                {
                    if (!CreateBacklogItem(kanbanTask))
                    {
                        return false;
                    }
                }
            }
            return true;
        }

        public  bool CreateBacklogItem(Backlogitem scrumwiseItem) 
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
                }
                else
                {
                    throw new Exception("Unknown priority");
                }


				var createResult = client.Execute<CreateBacklogItemResult>(req);
                if (createResult.IsSuccessful)
                {
                    string itemID = createResult.Data.Result;
                    scrumwiseItem.id = itemID;
                    foreach (var tagID in scrumwiseItem.tagIDs)
                    {
                        AddTag(itemID, tagID);
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
    }

    

}

