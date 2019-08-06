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
			List<Item> kanbanizeTasks = GetKanbanizeTasks("9", "til udvikler");
			
		}

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
