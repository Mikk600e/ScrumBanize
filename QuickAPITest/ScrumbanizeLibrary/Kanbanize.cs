﻿using QuickAPITest.Definitions;
using RestSharp;
using ScrumbanizeLibrary.Definitions;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuickAPITest
{
	public class Kanbanize
	{
		private string _boardID;
		private string _lane;
		private string _apiurl;
		private string _apiKey;
		private string _apiKeyValue;
		private string _backlogListID;
		private string _projectID;
		private string[] _scrumwiseKanbanizeTag;
		private string _scrumwiseRejectedTag;
		private Scrumwise _scrumwiseConnection;

		public Kanbanize(string kanbanizeBoardID, string kanbanizeLane, string scrumwiseBacklogListID,
			string scrumwiseProjectID, string kanbanizeAPI, string kanbanizeAPIKey, string kanbanizeAPIKeyValue, string scrumwiseKanbanizeTag, Scrumwise scrumwiseConnection, string scrumwiseRejectedTag)
		{

			this._boardID = kanbanizeBoardID;
			this._lane = kanbanizeLane;
			this._backlogListID = scrumwiseBacklogListID;
			this._projectID = scrumwiseProjectID;
			this._scrumwiseRejectedTag = scrumwiseRejectedTag;
			this._apiurl = kanbanizeAPI;
			this._apiKey = kanbanizeAPIKey;
			this._apiKeyValue = kanbanizeAPIKeyValue;
			this._scrumwiseKanbanizeTag = new string[1] { scrumwiseKanbanizeTag };

			this._scrumwiseConnection = scrumwiseConnection;
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
				//if externalID==null then externalID ="0" 
				if (container.externalID == null)
				{
					container.externalID = "0";
				}
				container.externalID = kanbasTask.TaskList[i].TaskId;
				container.priority = kanbasTask.TaskList[i].Priority;
				container.projectID = _projectID;
				container.tagIDs = _scrumwiseKanbanizeTag;
				container.name = kanbasTask.TaskList[i].Title;
				container.type = "Feature";			
				container.status = kanbasTask.TaskList[i].Columnname.ToLower();
				container = VariableFitter(container);
				scrumwiseItemList.TaskList.Add(container);
			}

			return scrumwiseItemList;
		}
		public bool CreateKanbanizeTasks(ScrumwiseItemList kanbanizeTaskList, ScrumwiseItemList scrumwiseItemList)
		{
			try
			{
				foreach (Backlogitem scrumwiseItem in scrumwiseItemList.TaskList)
				{
					if (!kanbanizeTaskList.TaskList.Exists(x => x.externalID.Equals(scrumwiseItem.externalID))) // If the Kanbanize task already exists in Scrumwise, don't try to create it again
					{
						CreateKanbanizeTask(scrumwiseItem);
					}
				}
				return true;
			}
			catch (Exception)
			{
				return false;
			}

		}
		private bool CreateKanbanizeTask(Backlogitem backlogitem)
		{
			KanbanizeTaskList kanbanizeTasks = new KanbanizeTaskList();
			KanbasID kanbasID = new KanbasID();
			backlogitem = VariableFitter(backlogitem);
			if (backlogitem.status == "arkiv")
			{
				return true;
			}
			RestClient client = new RestClient(_apiurl);
			RestRequest request = new RestRequest("create_new_task", Method.POST);
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
			var xmlDeserializer = new RestSharp.Deserializers.XmlDeserializer();
			kanbasID = xmlDeserializer.Deserialize<KanbasID>(response);
			backlogitem.externalID = kanbasID.id;
			_scrumwiseConnection.setBacklogItemExternalID(backlogitem);
			//Scrum.API.UpdateExternalID(backlogITem.ExternalID)
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
				case "Sprint completed":
					backlogitem.status = KanbanizeStatus.arkiv.ToString();
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
				case "Critical":
					backlogitem.priority = ScrumwisePriority.Urgent.ToString();
					break;
			}

			return backlogitem;
		}
		public bool KanbanizeMoveTasks(ScrumwiseItemList scrumwiseItemList)
		{
			try
			{
				KanbanizeTaskList kanbanizeTasks = new KanbanizeTaskList();
				kanbanizeTasks = GetKanbanizeTasks();
				for (int i = 0; i < scrumwiseItemList.TaskList.Count; i++)
				{
					KanbanizeMoveTask(scrumwiseItemList.TaskList[i], kanbanizeTasks);
				}
				return true;
			}
			catch (Exception)
			{
				return false;
				throw;
			}
		}
		private bool KanbanizeMoveTask(Backlogitem backlogitem, KanbanizeTaskList kanbanizeTask)
		{
			RestClient client = new RestClient(_apiurl);
			RestRequest request = new RestRequest("move_task", Method.POST);
			VariableFitter(backlogitem);
			foreach (Item itemToBeChecked in kanbanizeTask.TaskList)
			{
				if (backlogitem.externalID.ToString() == itemToBeChecked.TaskId)
				{
					if (itemToBeChecked.Columnname == "Arkiv" && backlogitem.status == KanbanizeStatus.done.ToString())
						return false;
				}
			}
			

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
		public bool KanbanizeCheckRejected(ScrumwiseItemList scrumwiseItemList)
		{
			KanbanizeTaskList container = GetKanbanizeTasks();
			foreach (Backlogitem backlogitem in scrumwiseItemList.TaskList)
			{
				if (backlogitem.tagIDs.Contains(_scrumwiseRejectedTag))
				{
					foreach (Item itemToBeChecked in container.TaskList)
					{
						if (itemToBeChecked.tags == null)
							itemToBeChecked.tags = "";
						if (itemToBeChecked.TaskId == backlogitem.externalID.ToString() && !itemToBeChecked.tags.Contains("Rejected"))
						{
							if (!KanbanizeEditTask(backlogitem))
								return false;
						}
					}
				}
			}
			return true;
		}
		private bool KanbanizeEditTask(Backlogitem backlogitem)
		{
			try
			{
				RestClient client = new RestClient(_apiurl);
				RestRequest request = new RestRequest("edit_task", Method.POST);
				request.AddHeader(_apiKey, _apiKeyValue);
				request.AddJsonBody(new
				{
					boardid = _boardID,
					taskid = backlogitem.externalID,
					tags = "Rejected"
				});
				var response = client.Post(request);
				return true;
			}
			catch (Exception)
			{
				return false;
				throw;
			}
		}
	}
}
