using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuickAPITest.Definitions
{
    public class Item
    {
        public string TaskId { get; set; } //v
		//public string ExternalId { get; set; }
		
        public string Priority { get; set; } //v
        public string Title { get; set; } //v
        public string Description { get; set; } //v
		public string ProjectId { get; set; } //v
		public List<Subtaskdetail> subtaskdetails { get; set; }

	}
}
