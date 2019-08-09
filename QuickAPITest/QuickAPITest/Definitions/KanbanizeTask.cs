using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuickAPITest.Definitions
{
    public class Item
    {
        public string TaskId { get; set; }
        public string Priority { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
		public string Columnname { get; set; }
    }
}
