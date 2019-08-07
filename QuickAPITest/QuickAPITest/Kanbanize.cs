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
		Scrumwise methodSlave; 

		public Kanbanize(string boardID, string lane)
		{
			
			this._boardID = boardID;
			this._lane = lane;
			KanbanizeTaskList items = GetKanbanizeTasks(boardID, lane);
			List<ScrumwiseItem> scrumwiseTask = ConvertKanbasToScrum(items);
			methodSlave.CreateBacklogItem(scrumwiseTask);
		}
		public KanbanizeTaskList GetKanbanizeTasks(string boardId, string laneName)
		{
			//List<Item> container = new List<Item>();
			KanbanizeTaskList kanbanizeTasks = new KanbanizeTaskList();
			var client = new RestClient("https://freeway.kanbanize.com/index.php/api/kanbanize");
			var request = new RestRequest("/get_all_tasks", Method.POST);
			request.AddHeader("apikey", "mMt64VOgJK4pqlSKhnE6XUCoLDCOcbAEoFUtUJjI");
			request.AddJsonBody(new { boardid = boardId, lane = laneName });
			var response = client.Post(request);
			var xmlDeserializer = new RestSharp.Deserializers.XmlDeserializer();
			return kanbanizeTasks = xmlDeserializer.Deserialize<KanbanizeTaskList>(response);
			//container.AddRange(result.TaskList);
		}
		public List<ScrumwiseItem> ConvertKanbasToScrum(KanbanizeTaskList kanbasTask)
		{
			List<ScrumwiseItem> scrumItems = new List<ScrumwiseItem>();
			for (int i = 0; i < kanbasTask.TaskList.Count; i++)
			{
				ScrumwiseItem container = new ScrumwiseItem();
				container.BacklogListId = "191469-2531-15";
				container.Description = kanbasTask.TaskList[i].Description;
				container.ExternalId = kanbasTask.TaskList[i].TaskId;
				container.Priority = ScrumwisePriority.High;
				container.ProjectId = "191469-0-5";
				container.TagIds = new string[1] { "191469-2533-1" };
				container.Title = kanbasTask.TaskList[i].Title;
				container.Type = "Bug";
				scrumItems.Add(container);
			}
			return scrumItems;
		}
	}
}
