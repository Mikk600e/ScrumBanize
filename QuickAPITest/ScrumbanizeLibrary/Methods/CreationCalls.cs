using QuickAPITest.Definitions;
using RestSharp;
using ScrumbanizeLibrary.Definitions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ScrumbanizeLibrary.Methods
{
	class CreationCalls
	{
		private bool CreateKanbanizeTask(Backlogitem backlogitem)
		{
			KanbanizeTaskList kanbanizeTasks = new KanbanizeTaskList();
			KanbasID kanbasID = new KanbasID();
			Converter convert = new Converter();
			RestClient client = new RestClient(ScrumbanizeConnector._kanbanApiURL);
			RestRequest request = new RestRequest("create_new_task", Method.POST);
			backlogitem = convert.VariableFitter(backlogitem);
			request.AddHeader(ScrumbanizeConnector._apiKey, ScrumbanizeConnector._KanbanApiKeyValue);
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
	}
}
