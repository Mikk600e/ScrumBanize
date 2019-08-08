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
		private string _apiurl;
		private string _apiKey;
		private string _apiKeyValue;

		public Kanbanize(string boardID, string lane, string apiurl, string apiKey, string apiKeyValue)
		{
			
			this._boardID = boardID;
			this._lane = lane;
			this._apiurl = apiurl;
			this._apiKey = apiKey;
			this._apiKeyValue = apiKeyValue;
			GetKanbanizeTasks();
		}
		public KanbanizeTaskList GetKanbanizeTasks()
		{
			KanbanizeTaskList kanbanizeTasks = new KanbanizeTaskList();
			var client = new RestClient(_apiurl);
			var request = new RestRequest("/get_all_tasks", Method.POST);
			request.AddHeader(_apiKey, _apiKeyValue);
			request.AddJsonBody(new { boardid = _boardID, lane = _lane });
			var response = client.Post(request);
			var xmlDeserializer = new RestSharp.Deserializers.XmlDeserializer();
			return kanbanizeTasks = xmlDeserializer.Deserialize<KanbanizeTaskList>(response);
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
		public bool CreateKanbanizeTask()
		{
			RestRequest client = new RestRequest(_apiurl);

			return true;
		}
	}
}
