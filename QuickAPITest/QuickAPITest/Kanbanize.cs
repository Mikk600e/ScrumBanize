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
		}
		public KanbanizeTaskList GetKanbanizeTasks()
		{
			KanbanizeTaskList kanbanizeTasks = new KanbanizeTaskList();
			RestClient client = new RestClient(_apiurl);
			RestRequest request = new RestRequest("/get_all_tasks", Method.POST);
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
		// TODO lav foreach for hver dimmer i tingen
		public bool CreateKanbanizeTask(ScrumwiseItemList scrumwiseItemList)
		{
			RestClient client = new RestClient(_apiurl);
			RestRequest request = new RestRequest("create_new_task", Method.POST);
			scrumwiseItemList = VariableFitter(scrumwiseItemList);
			request.AddHeader(_apiKey, _apiKeyValue);
			request.AddJsonBody(new
			{
				boardid = _boardID,
				column = scrumwiseItemList.TaskList[0].status,
				lane = _lane,
				priority = scrumwiseItemList.TaskList[0].priority,
				type = scrumwiseItemList.TaskList[0].type,
				title = scrumwiseItemList.TaskList[0].name,
				description = scrumwiseItemList.TaskList[0].description
			});
			var response = client.Post(request);
			return true;
		}
		public ScrumwiseItemList VariableFitter(ScrumwiseItemList scrumwiseItemList)
		{
			for (int i = 0; i < scrumwiseItemList.TaskList.Count; i++)
			{
				switch (scrumwiseItemList.TaskList[i].status)
				{
					case "To do":
						scrumwiseItemList.TaskList[i].status = KanbanizeStatus.planlagt.ToString();
						break;
					case "In progress":
						scrumwiseItemList.TaskList[i].status = KanbanizeStatus.igang.ToString().Insert(1," ");
						break;
					case "Done":
						break;
				}
			}
			return scrumwiseItemList;
		}
	}
}
