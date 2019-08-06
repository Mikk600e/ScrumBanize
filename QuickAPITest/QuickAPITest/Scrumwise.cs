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

        public bool CreateBacklogItem(string title, string description, ScrumwisePriority priority, string projectId, string backlogListId, params string[] tagIDs)
        {
            try
            {
                RestClient client = new RestClient(_apiurl);
                client.Authenticator = new HttpBasicAuthenticator(_userName, _key);

                RestRequest req = new RestRequest("addBacklogItem", Method.POST);
                req.AddParameter("projectID", projectId);
                req.AddParameter("backlogListID", backlogListId);
                req.AddParameter("type", "Bug");
                req.AddParameter("name", title);
                req.AddParameter("description", description);
                switch (priority)
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
                    foreach (var tagID in tagIDs)
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

