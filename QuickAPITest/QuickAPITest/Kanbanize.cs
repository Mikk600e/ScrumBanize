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
			KanbanizeTaskList items = GetKanbanizeTasks();
			ScrumwiseItemList scrumwiseTask = ConvertKanbasToScrum(items);
			//methodSlave.CreateBacklogItem(scrumwiseTask);
		}
		public KanbanizeTaskList GetKanbanizeTasks()
		{
			//List<Item> container = new List<Item>();
			KanbanizeTaskList kanbanizeTasks = new KanbanizeTaskList();
			var client = new RestClient("https://freeway.kanbanize.com/index.php/api/kanbanize");
			var request = new RestRequest("/get_all_tasks", Method.POST);
			request.AddHeader("apikey", "mMt64VOgJK4pqlSKhnE6XUCoLDCOcbAEoFUtUJjI");
			request.AddJsonBody(new { boardid = _boardID, lane = _lane });
			var response = client.Post(request);
			var xmlDeserializer = new RestSharp.Deserializers.XmlDeserializer();
			return kanbanizeTasks = xmlDeserializer.Deserialize<KanbanizeTaskList>(response);
			//container.AddRange(result.TaskList);
		}
		public ScrumwiseItemList ConvertKanbasToScrum(KanbanizeTaskList kanbasTask)
		{
			ScrumwiseItemList scrumwiseItemList = new ScrumwiseItemList();
			scrumwiseItemList.TaskList = new List<Backlogitem>();
			for (int i = 0; i < kanbasTask.TaskList.Count; i++)
			{
				
				Backlogitem container = new Backlogitem();
				container.backlogListID = "191469-2531-15";
				container.description = kanbasTask.TaskList[i].Description;
				container.externalID = kanbasTask.TaskList[i].TaskId;
				container.priority = ScrumwisePriority.High.ToString();
				container.projectID = "191469-0-5";
				container.tagIDs = new string[1] { "191469-2533-1" };
				container.name = kanbasTask.TaskList[i].Title;
				container.type = "Bug";
				scrumwiseItemList.TaskList.Add(container);
			}
			
			return scrumwiseItemList;
		}
	}
}
