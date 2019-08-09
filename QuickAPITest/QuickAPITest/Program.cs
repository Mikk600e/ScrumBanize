using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;
using QuickAPITest.Definitions;
using RestSharp;
using RestSharp.Authenticators;
using RestSharp.Serialization;
using RestSharp.Deserializers;
using System.Xml;
using System.Xml.Linq;
namespace QuickAPITest
{
	class Program
	{
        


        static void Main(string[] args)
		{
            //ConfigurationManager.
            string scrumwiseUser = ConfigurationManager.AppSettings.Get("scrumwiseUser");//"niels@freeway.dk";
            string scrumwiseKey = ConfigurationManager.AppSettings["scrumwiseKey"]; //"591D5EAF2868B8B0531D9B0BF03A017AE3F87BFFDF6A4919FC375CB6229B7351";
            string scrumwiseAPI = ConfigurationManager.AppSettings["scrumwiseAPI"]; //"https://api.scrumwise.com/service/api/v1/";
            string scrumwiseKanbanizeTag = ConfigurationManager.AppSettings["scrumwiseKanbanizeTag"]; //"191469-2533-1";
            string scrumwiseProjectID = ConfigurationManager.AppSettings["scrumwiseProjectID"]; //"191469-0-5";
            string kanbanizeBoardId = ConfigurationManager.AppSettings["kanbanizeBoardID"]; //"9";
            string kanbanizeLane = ConfigurationManager.AppSettings["kanbanizeLane"]; //"til udvikler";
            string kanbanizeAPI = ConfigurationManager.AppSettings["kanbanizeAPI"]; //"https://freeway.kanbanize.com/index.php/api/kanbanize";
            string kanbanizeAPIKey = ConfigurationManager.AppSettings["kanbanizeAPIKey"]; //"apikey";
            string kanbanizeAPIKeyValue = ConfigurationManager.AppSettings["kanbanizeAPIKeyValue"]; //"mMt64VOgJK4pqlSKhnE6XUCoLDCOcbAEoFUtUJjI";


			Kanbanize kanbanizeConnection = new Kanbanize(kanbanizeBoardId, kanbanizeLane, kanbanizeAPI, kanbanizeAPIKey, kanbanizeAPIKeyValue);
			Scrumwise scrumwiseConnection = new Scrumwise(scrumwiseUser, scrumwiseKey, scrumwiseAPI);
			kanbanizeConnection.KanbanizeMoveTasks(scrumwiseConnection.GetKanbanizeItemsInScrumwise(scrumwiseKanbanizeTag,scrumwiseProjectID));
            scrumwiseConnection.ImportKanbanizeToScrumwise(kanbanizeConnection.ConvertKanbasToScrum(kanbanizeConnection.GetKanbanizeTasks()), scrumwiseConnection.GetKanbanizeItemsInScrumwise(scrumwiseKanbanizeTag, scrumwiseProjectID));
            //kanbanizeConnection.CreateKanbanizeTasks(scrumwiseConnection.GetKanbanizeItemsInScrumwise(scrumwiseKanbanizeTag, scrumwiseProjectID));

		}
    }
}