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
            // List<KanbanizeTask> kanbanizeTasks = GetKanbanizeTasks("9", "til udvikler");

            Scrumwise scrumwiseConnection = new Scrumwise("niels@freeway.dk", "591D5EAF2868B8B0531D9B0BF03A017AE3F87BFFDF6A4919FC375CB6229B7351", "https://api.scrumwise.com/service/api/v1/");
            /*ScrumwiseItem test = new ScrumwiseItem();
            test.Title = "Test From Script";
            test.Description = "Created from the script";
            test.BacklogListId = "191469-2531-15";
            test.ProjectId = "191469-0-5";
            test.Priority = ScrumwisePriority.Normal;
            bool succes = scrumwiseConnection.CreateBacklogItem(test);*/

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
