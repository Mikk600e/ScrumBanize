using QuickAPITest;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ScrumbanizeLibrary
{
	public class ScrumbanizeConnector
	{
		public static string _scrumwiseApiURL;
		public static string _userName;
		public static string _key;
		public static string _kanbanizeTagID;
		public static string _projectID;
		public static string _backlogListID;
		public static string _templateTagID;
		public static string _teamID;
		public static string _estimateUnit;

		public static string _kanbanApiURL;
		public static string _boardID;
		public static string _lane;
		public static string _kanbanApiKey;
		public static string _KanbanApiKeyValue;
		public static string _type;
		public static string[] _scrumwiseKanbanizeTag;
		public ScrumbanizeConnector(string scrumwiseProjectID, string scrumwiseUser, string scrumwiseKey, string scrumwiseAPI, string scrumwiseBacklogListID, string scrumwiseKanbanizeTag
			, string kanbanizeBoardID, string kanbanizeLane, string kanbanizeAPI, string kanbanizeAPIKey, string kanbanizeAPIKeyValue)
		{
			_userName = scrumwiseUser;
			_key = scrumwiseKey;
			_scrumwiseApiURL = scrumwiseAPI;
			_kanbanizeTagID = scrumwiseKanbanizeTag;
			_projectID = scrumwiseProjectID;
			_backlogListID = scrumwiseBacklogListID;

			_boardID = kanbanizeBoardID;
			_lane = kanbanizeLane;
			_kanbanApiURL = kanbanizeAPI;
			_apiKey = kanbanizeAPIKey;
			_KanbanApiKeyValue = kanbanizeAPIKeyValue;
			_scrumwiseKanbanizeTag = new string[1] { scrumwiseKanbanizeTag };
			_type = "Bug";

			Kanbanize kanbanizeConnection = new Kanbanize();
			Scrumwise scrumwiseConnection = new Scrumwise();
			
		}
	}
}
