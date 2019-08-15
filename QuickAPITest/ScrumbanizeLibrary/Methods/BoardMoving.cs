using QuickAPITest.Definitions;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ScrumbanizeLibrary.Methods
{
	class BoardMoving
	{
		private bool KanbanizeMoveTask(Backlogitem backlogitem)
		{
			RestClient client = new RestClient(ScrumbanizeConnector._kanbanApiURL);
			RestRequest request = new RestRequest("move_task", Method.POST);
			Converter convert = new Converter();
			convert.VariableFitter(backlogitem);
			request.AddHeader(ScrumbanizeConnector._apiKey, ScrumbanizeConnector._KanbanApiKeyValue);
			request.AddJsonBody(new
			{
				boardid = ScrumbanizeConnector._boardID,
				taskid = backlogitem.externalID,
				column = backlogitem.status
			});
			var response = client.Post(request);
			return false;
		}
	}
}
