using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
            string scrumwiseUser = "niels@freeway.dk";
            string scrumwiseKey = "591D5EAF2868B8B0531D9B0BF03A017AE3F87BFFDF6A4919FC375CB6229B7351";
            string scrumwiseAPI = "https://api.scrumwise.com/service/api/v1/";
            string scrumwiseKanbanizeTag = "191469-2533-1";
            string scrumwiseProjectID = "191469-0-5";
            string kanbanizeBoardId = "9";
            string kanbanizeLane = "til udvikler";
			string kanbanizeAPI = "https://freeway.kanbanize.com/index.php/api/kanbanize";
			string kanbanizeAPIKey = "apikey";
			string kanbanizeAPIKeyValue = "mMt64VOgJK4pqlSKhnE6XUCoLDCOcbAEoFUtUJjI";


			Kanbanize kanbanizeConnection = new Kanbanize(kanbanizeBoardId, kanbanizeLane, kanbanizeAPI, kanbanizeAPIKey, kanbanizeAPIKeyValue);
			//KanbanizeTaskList kanbanizeTaskList = kanbanizeConnection.GetKanbanizeTasks();
			//ScrumwiseItemList scrumwiseItemList = kanbanizeConnection.ConvertKanbasToScrum(kanbanizeTaskList);
			//List<ScrumwiseItemList> scrumwiseItemLists = kanbanize.ConvertKanbasToScrum(kanbanizeTaskList);
			Scrumwise scrumwiseConnection = new Scrumwise(scrumwiseUser, scrumwiseKey, scrumwiseAPI);
			ScrumwiseItemList test = scrumwiseConnection.GetKanbanizeItemsInScrumwise(scrumwiseKanbanizeTag, scrumwiseProjectID);
			kanbanizeConnection.CreateKanbanizeTask(test);
			//scrumwiseConnection.CreateBacklogItem(scrumwiseItemList.TaskList);
			//scrumwiseConnection.GetKanbanizeItemsInScrumwise(scrumwiseKanbanizeTag);
		}
	}
}