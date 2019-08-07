using QuickAPITest.Definitions;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuickAPITest
{
	class Kanbanize
	{
		private string _boardID;
		private string _lane;

		public Kanbanize(string boardID, string lane)
		{
			this._boardID = boardID;
			this._lane = lane;
		}
		public bool GetKanbanizeTasks(string boardId, string laneName)
		{
			//List<Item> container = new List<Item>();
			KanbanizeTaskList kanbanizeTasks = new KanbanizeTaskList();
			var client = new RestClient("https://freeway.kanbanize.com/index.php/api/kanbanize");
			var request = new RestRequest("/get_all_tasks", Method.POST);
			request.AddHeader("apikey", "mMt64VOgJK4pqlSKhnE6XUCoLDCOcbAEoFUtUJjI");
			request.AddJsonBody(new { boardid = boardId, lane = laneName });
			var response = client.Post(request);
			var xmlDeserializer = new RestSharp.Deserializers.XmlDeserializer();

			kanbanizeTasks = xmlDeserializer.Deserialize<KanbanizeTaskList>(response);
			//container.AddRange(result.TaskList);


			return true;
		}

	}
}
