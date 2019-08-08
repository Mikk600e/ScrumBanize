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

        public  bool CreateBacklogItem(List<Backlogitem> scrumwiseItem) 
        {
            try
            {
				
                RestClient client = new RestClient(_apiurl);
                client.Authenticator = new HttpBasicAuthenticator(_userName, _key);
                //RestRequest req = new RestRequest("addBacklogItem", Method.POST);
				for (int i = 0; i < scrumwiseItem.Count; i++)
				{
					RestRequest req = new RestRequest("addBacklogItem", Method.POST);

					req.AddParameter("projectID", scrumwiseItem[i].projectID);
					req.AddParameter("backlogListID", scrumwiseItem[i].backlogListID);
					req.AddParameter("externalID", scrumwiseItem[i].externalID);
					req.AddParameter("type", scrumwiseItem[i].type);
					req.AddParameter("name", scrumwiseItem[i].name);
					req.AddParameter("description", scrumwiseItem[i].description);

                    if (scrumwiseItem[i].priority == ScrumwisePriority.Normal.ToString())
                    {

                    }
                    else if (scrumwiseItem[i].priority == ScrumwisePriority.High.ToString())
                    {
                        req.AddParameter("priority", "High");
                    }
                    else if (scrumwiseItem[i].priority == ScrumwisePriority.Urgent.ToString())
                    {
                        req.AddParameter("priority", "Urgent");
                    }
                    else
                    {
                        throw new Exception("Unknown priority");
                    }

                    /*switch (scrumwiseItem[i].priority)
					{
						case ScrumwisePriority.Normal.ToString(): break;//no specific priority for this req.AddParameter("priority", "Medi");
						case ScrumwisePriority.High.ToString(): req.AddParameter("priority", "High"); break;
						case ScrumwisePriority.Urgent.ToString(): req.AddParameter("priority", "Urgent"); break;
						default:
							throw new Exception("Unknown priority");
					}*/

					var createResult = client.Execute<CreateBacklogItemResult>(req);

				}
				//req.AddParameter("projectID", scrumwiseItem[i].ProjectId);
				//req.AddParameter("backlogListID", scrumwiseItem.BacklogListId);
				//req.AddParameter("externalID", scrumwiseItem.ExternalId);
				//req.AddParameter("type", scrumwiseItem.Type);
				//req.AddParameter("name", scrumwiseItem.Title);
				//req.AddParameter("description", scrumwiseItem.Description);

				//switch (scrumwiseItem.Priority)
				//{
				//    case ScrumwisePriority.Normal: break;//no specific priority for this req.AddParameter("priority", "Medi");
				//    case ScrumwisePriority.High: req.AddParameter("priority", "High"); break;
				//    case ScrumwisePriority.Urgent: req.AddParameter("priority", "Urgent"); break;
				//    default:
				//        throw new Exception("Unknown priority");
				//}

				//var createResult = client.Execute<CreateBacklogItemResult>(req);

                //if (createResult.IsSuccessful)
                //{
                //    string itemID = createResult.Data.Result;
                //    scrumwiseItem.ItemId = itemID;
                //    foreach (var tagID in scrumwiseItem.TagIds)
                //    {
                //        AddTag(itemID, tagID);
                //    }

                //    return true;
                //}
            }
            catch (Exception)
            {

                throw;
            }

            return false;
        }

        public bool GetKanbanizeItemsInScrumwise(string kanbanizeTagId, string scrumwiseProjectId)
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
            //foreach(Backlogitem backlogitem in test.result.projects[0].backlogItems)
            //{
            //    if (backlogitem.tagIDs.Contains(kanbanizeTagId))
            //    {

            //    }
            //}

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

