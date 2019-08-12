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
            string[] boardID = ConfigurationManager.AppSettings["kanbanizeBoardID"].Split(',');
            string[] lane = ConfigurationManager.AppSettings["kanbanizeLane"].Split(',');
            string[] backlogListID = ConfigurationManager.AppSettings["scrumwiseBacklogListID"].Split(',');
            string[] projectID = ConfigurationManager.AppSettings["scrumwiseProjectID"].Split(',');
            for(int i = 0; i < boardID.Count(); i++)
            {
                Kanbanize kanbanizeConnection = new Kanbanize(boardID[i], lane[i], backlogListID[i], projectID[i]);
                Scrumwise scrumwiseConnection = new Scrumwise(projectID[i]);
                scrumwiseConnection.ImportKanbanizeToScrumwise(kanbanizeConnection.ConvertKanbasToScrum(kanbanizeConnection.GetKanbanizeTasks()), scrumwiseConnection.GetKanbanizeItemsInScrumwise());
                kanbanizeConnection.CreateKanbanizeTasks(kanbanizeConnection.ConvertKanbasToScrum(kanbanizeConnection.GetKanbanizeTasks()), scrumwiseConnection.GetKanbanizeItemsInScrumwise());
                kanbanizeConnection.KanbanizeMoveTasks(scrumwiseConnection.GetKanbanizeItemsInScrumwise());
            }
		}
	}
}