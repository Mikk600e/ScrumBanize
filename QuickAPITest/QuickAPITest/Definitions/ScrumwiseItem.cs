using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuickAPITest.Definitions
{
    class ScrumwiseItem
    {
        public string ItemId { get; set; }
        public string ProjectId { get; set; }
        public string BacklogListId { get; set; }
        public ScrumwisePriority Priority { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string Type { get; set; }
        public string[] TagIds { get; set; }
    }
}
