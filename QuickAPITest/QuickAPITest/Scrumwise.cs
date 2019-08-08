using QuickAPITest.Definitions;
using RestSharp;
using RestSharp.Authenticators;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace QuickAPITest
{
    
    class Scrumwise
    {

        private string _apiurl;
        private string _userName;
        private string _key;

        public Scrumwise(string userName, string key, string apiurl) // apiurl: https://api.scrumwise.com/service/api/v1/
        {
            this._userName = userName;
            this._key = key;
            this._apiurl = apiurl;
        }

        public bool ImportKanbanizeToScrumwise(ScrumwiseItemList kanbanizeTaskList, ScrumwiseItemList scrumwiseItemList)
        {
            foreach (Backlogitem kanbanTask in kanbanizeTaskList.TaskList)
            {
                if (scrumwiseItemList.TaskList.Exists(x => x.externalID == kanbanTask.externalID))
                {
                    CreateBacklogItem(kanbanTask);
                }
            }
            return false;
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

                if (scrumwiseItem.priority == ScrumwisePriority.Normal.ToString())
                {

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

        public ScrumwiseItemList GetKanbanizeItemsInScrumwise(string kanbanizeTagId, string scrumwiseProjectId)
        {
            ScrumwiseItemList scrumwiseItemList = new ScrumwiseItemList();
            scrumwiseItemList.TaskList = new List<Backlogitem>();

            RestClient client = new RestClient(_apiurl);
            client.Authenticator = new HttpBasicAuthenticator(_userName, _key);

            RestRequest request = new RestRequest("getData", Method.POST);
            request.AddParameter("projectIDs", scrumwiseProjectId);
            request.AddParameter("includeProperties", "Project.backlogItems,BacklogItem.tasks");
            var response = client.Post(request);
            Rootobject test = SimpleJson.DeserializeObject<Rootobject>(response.Content);
            foreach(Backlogitem backlogitem in test.result.projects[0].backlogItems)
            {
                if (backlogitem.tagIDs.Contains(kanbanizeTagId))
                {
                    scrumwiseItemList.TaskList.Add(backlogitem);
                }
            }

            return scrumwiseItemList;
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

