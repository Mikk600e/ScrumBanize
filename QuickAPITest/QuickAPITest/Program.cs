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
			Kanbanize kanbanizeConnection = new Kanbanize();
			Scrumwise scrumwiseConnection = new Scrumwise();
			kanbanizeConnection.KanbanizeMoveTasks(scrumwiseConnection.GetKanbanizeItemsInScrumwise());
			scrumwiseConnection.ImportKanbanizeToScrumwise(kanbanizeConnection.ConvertKanbasToScrum(kanbanizeConnection.GetKanbanizeTasks()), scrumwiseConnection.GetKanbanizeItemsInScrumwise());
		}
	}
}