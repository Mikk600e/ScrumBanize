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
<<<<<<< HEAD
            // List<KanbanizeTask> kanbanizeTasks = GetKanbanizeTasks("9", "til udvikler");

            Scrumwise scrumwiseConnection = new Scrumwise("niels@freeway.dk", "591D5EAF2868B8B0531D9B0BF03A017AE3F87BFFDF6A4919FC375CB6229B7351", "https://api.scrumwise.com/service/api/v1/");
            /*ScrumwiseItem test = new ScrumwiseItem();
            test.Title = "Test From Script";
            test.Description = "Created from the script";
            test.BacklogListId = "191469-2531-15";
            test.ProjectId = "191469-0-5";
            test.Priority = ScrumwisePriority.Normal;
            bool succes = scrumwiseConnection.CreateBacklogItem(test);*/

        }
=======
			List<Item> kanbanizeTasks = GetKanbanizeTasks("9", "til udvikler");
			
		}
>>>>>>> fdc5f829c21540cfebc36a042842906e2e73289e

		static List<Item> GetKanbanizeTasks(string boardId, string laneName)
		{
			List<Item> container = new List<Item>();
			KanbanizeTaskList kanbanizeTasks = new KanbanizeTaskList();
			var client = new RestClient("https://freeway.kanbanize.com/index.php/api/kanbanize");
			var request = new RestRequest("/get_all_tasks", Method.POST);
			request.AddHeader("apikey", "mMt64VOgJK4pqlSKhnE6XUCoLDCOcbAEoFUtUJjI");
			request.AddJsonBody(new { boardid = boardId, lane = laneName });
			var response = client.Post(request);
			var xmlDeserializer = new RestSharp.Deserializers.XmlDeserializer();
			/*
			 * foreach result
			 * new Kanb anizerTask = result
			 * add task to list
			 */
			 
			var result = xmlDeserializer.Deserialize<KanbanizeTaskList>(response);
			container.AddRange(result.TaskList);
			

			return container;
		}
	}
}
