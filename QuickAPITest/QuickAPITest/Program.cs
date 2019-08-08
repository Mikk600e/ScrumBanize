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

            Kanbanize kanbanize = new Kanbanize(kanbanizeBoardId, kanbanizeLane);
			KanbanizeTaskList kanbanizeTaskList = kanbanize.GetKanbanizeTasks();
			ScrumwiseItemList scrumwiseItemList = kanbanize.ConvertKanbasToScrum(kanbanizeTaskList);
			//List<ScrumwiseItemList> scrumwiseItemLists = kanbanize.ConvertKanbasToScrum(kanbanizeTaskList);
			Scrumwise scrumwiseConnection = new Scrumwise(scrumwiseUser, scrumwiseKey, scrumwiseAPI);
<<<<<<< HEAD
            scrumwiseConnection.GetKanbanizeItemsInScrumwise(scrumwiseKanbanizeTag, scrumwiseProjectID);
=======
			scrumwiseConnection.CreateBacklogItem(scrumwiseItemList.TaskList);
            scrumwiseConnection.GetKanbanizeItemsInScrumwise(scrumwiseKanbanizeTag);
>>>>>>> 16fa864e35f248668fea90356683856561faa2d3
        }
	}
}