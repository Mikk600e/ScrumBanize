using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using QuickAPITest.Definitions;
using RestSharp;
using RestSharp.Authenticators;

namespace QuickAPITest
{
	class Program
	{
		static void Main(string[] args)
		{
            List<KanbanizeTask> kanbanizeTasks = GetKanbanizeTasks("9", "til udvikler");

		}

        static List<KanbanizeTask> GetKanbanizeTasks( string boardId, string laneName)
        {
            List<KanbanizeTask> kanbanizeTasks = new List<KanbanizeTask>();

            var client = new RestClient("https://freeway.kanbanize.com/index.php/api/kanbanize");
            var request = new RestRequest("/get_all_tasks", Method.POST);
            request.AddHeader("apikey", "mMt64VOgJK4pqlSKhnE6XUCoLDCOcbAEoFUtUJjI");
            request.AddJsonBody(new { boardid = boardId, lane = laneName });
            var response = client.Post(request);

            return kanbanizeTasks;
        }

        
	}
}
