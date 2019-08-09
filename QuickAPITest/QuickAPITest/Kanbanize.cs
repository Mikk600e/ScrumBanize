using QuickAPITest.Definitions;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Configuration;
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
		private string _backlogListID;
		private string[] _scrumwiseKanbanizeTag;

		public Kanbanize()
		{

			this._boardID = ConfigurationManager.AppSettings["kanbanizeBoardID"];
			this._lane = ConfigurationManager.AppSettings["kanbanizeLane"];
			this._apiurl = ConfigurationManager.AppSettings["kanbanizeAPI"];
			this._apiKey = ConfigurationManager.AppSettings["kanbanizeAPIKey"];
			this._apiKeyValue = ConfigurationManager.AppSettings["kanbanizeAPIKeyValue"];
			this._backlogListID = ConfigurationManager.AppSettings["scrumwiseBacklogListID"];
			this._scrumwiseKanbanizeTag = new string[1] { ConfigurationManager.AppSettings["scrumwiseKanbanizeTag"] };



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
				container.backlogListID = _backlogListID;
				container.description = kanbasTask.TaskList[i].Description;
				container.externalID = kanbasTask.TaskList[i].TaskId;
				container.priority = kanbasTask.TaskList[i].Priority;
				container.projectID = ConfigurationManager.AppSettings[""];
				container.tagIDs = _scrumwiseKanbanizeTag;
				container.name = kanbasTask.TaskList[i].Title;
				container.type = "Bug";
				container.status = kanbasTask.TaskList[i].Columnname.ToLower();
				container = VariableFitter(container);
				scrumwiseItemList.TaskList.Add(container);
			}

			return scrumwiseItemList;
		}
		public bool CreateKanbanizeTasks(ScrumwiseItemList scrumwiseItemList)
		{
			try
			{
				for (int i = 0; i < scrumwiseItemList.TaskList.Count; i++)
				{
					CreateKanbanizeTask(scrumwiseItemList.TaskList[i]);
				}
				return true;
			}
			catch (Exception)
			{
				return false;
				throw;
			}
			
		}
		private bool CreateKanbanizeTask(Backlogitem backlogitem)
		{
			RestClient client = new RestClient(_apiurl);
			RestRequest request = new RestRequest("create_new_task", Method.POST);
			backlogitem = VariableFitter(backlogitem);
			request.AddHeader(_apiKey, _apiKeyValue);
			request.AddJsonBody(new
			{
				boardid = _boardID,
				column = backlogitem.status,
				lane = _lane,
				priority = backlogitem.priority,
				type = backlogitem.type,
				title = backlogitem.name,
				description = backlogitem.description
			});
			var response = client.Post(request);
			return true;
		}
		private Backlogitem VariableFitter(Backlogitem backlogitem)
		{
			switch (backlogitem.status)
			{
				case "To do":
					backlogitem.status = KanbanizeStatus.planlagt.ToString();
					break;
				case "In progress":
					backlogitem.status = KanbanizeStatus.igang.ToString().Insert(1, " ");
					break;
				case "Done":
					backlogitem.status = KanbanizeStatus.done.ToString();
					break;
				case "New":
					backlogitem.status = KanbanizeStatus.planlagt.ToString();
					break;
			}
			switch (backlogitem.priority)
			{
				case "Low":
					backlogitem.priority = ScrumwisePriority.Low.ToString();
					break;
				case "Average":
					backlogitem.priority = ScrumwisePriority.Medium.ToString();
					break;
				case "High":
					backlogitem.priority = ScrumwisePriority.High.ToString();
					break;
				case "Urgent":
					backlogitem.priority = ScrumwisePriority.Urgent.ToString();
					break;
			}

			return backlogitem;
		}
		public bool KanbanizeMoveTasks(ScrumwiseItemList scrumwiseItemList)
		{
			try
			{
				for (int i = 0; i < scrumwiseItemList.TaskList.Count; i++)
				{
					KanbanizeMoveTask(scrumwiseItemList.TaskList[i]);
				}
				return true;
			}
			catch (Exception)
			{
				return false;
				throw;
			}
		}
		private bool KanbanizeMoveTask(Backlogitem backlogitem)
		{
			RestClient client = new RestClient(_apiurl);
			RestRequest request = new RestRequest("move_task", Method.POST);
			VariableFitter(backlogitem);
			request.AddHeader(_apiKey, _apiKeyValue);
			request.AddJsonBody(new
			{
				boardid = _boardID,
				taskid = backlogitem.externalID,
				column = backlogitem.status
			});
			var response = client.Post(request);
			return false;
		}
	}
}
