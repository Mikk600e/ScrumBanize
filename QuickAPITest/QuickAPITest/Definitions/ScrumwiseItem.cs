using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuickAPITest.Definitions
{
    class ScrumwiseItem
    {
        public string ItemId { get; set; } //v
        public string ProjectId { get; set; }
		//191469-0-5
		public string BacklogListId { get; set; }
		//191469-2531-15
		public string ExternalId { get; set; } //V
        public ScrumwisePriority Priority { get; set; } //V
        public string Title { get; set; }  //v
        public string Description { get; set; } //v
        public string Type { get; set; }
		//Doesn't exist when a task in Kanbannize is manually made, could make it bug per default, but what if isn't? 
        public string[] TagIds { get; set; }
        //191469-2533-1 Kanbanize tag
    }
}
