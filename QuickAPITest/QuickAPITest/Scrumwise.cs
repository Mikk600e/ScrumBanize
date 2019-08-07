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

        public bool CreateBacklogItem(ScrumwiseItem scrumwiseItem) 
        {
            try
            {
                RestClient client = new RestClient(_apiurl);
                client.Authenticator = new HttpBasicAuthenticator(_userName, _key);

                RestRequest req = new RestRequest("addBacklogItem", Method.POST);
                req.AddParameter("projectID", scrumwiseItem.ProjectId);
                req.AddParameter("backlogListID", scrumwiseItem.BacklogListId);
                req.AddParameter("externalID", scrumwiseItem.ExternalId);
                req.AddParameter("type", scrumwiseItem.Type);
                req.AddParameter("name", scrumwiseItem.Title);
                req.AddParameter("description", scrumwiseItem.Description);
                switch (scrumwiseItem.Priority)
                {
                    case ScrumwisePriority.Normal: break;//no specific priority for this req.AddParameter("priority", "Medi");
                    case ScrumwisePriority.High: req.AddParameter("priority", "High"); break;
                    case ScrumwisePriority.Urgent: req.AddParameter("priority", "Urgent"); break;
                    default:
                        throw new Exception("Unknown priority");
                }

                var createResult = client.Execute<CreateBacklogItemResult>(req);

                if (createResult.IsSuccessful)
                {
                    string itemID = createResult.Data.Result;
                    scrumwiseItem.ItemId = itemID;
                    foreach (var tagID in scrumwiseItem.TagIds)
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

        public bool GetKanbanizeItemsInScrumwise(string kanbanizeTagId)
        {
            Projects scrumwiseItemList = new Projects();
            RestClient client = new RestClient(_apiurl);
            client.Authenticator = new HttpBasicAuthenticator(_userName, _key);

            RestRequest request = new RestRequest("getData", Method.POST);
            //request.AddParameter("ProjectIDs", "191469-0-5");
            request.AddParameter("includeProperties", "Project.backlogItems,BacklogItem.tasks");
            var response = client.Post(request);
            IRestResponse<Projects> response2 = client.Execute<Projects>(request);
            var test = SimpleJson.DeserializeObject(response.Content);
            //ScrumwiseItem testItem = new ScrumwiseItem();
            
            //var testItem= test.Values[3];
            //scrumwiseItemList = response.Content;
            var xmlDeserializer = new RestSharp.Deserializers.XmlDeserializer();
            

            scrumwiseItemList = xmlDeserializer.Deserialize<Projects>(response);

            return false;
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

