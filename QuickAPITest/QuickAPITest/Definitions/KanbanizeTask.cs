using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace QuickAPITest.Definitions
{
	
    public class Item
    {
		[XmlElement("TaskId")]
        public string TaskId { get; set; }
		[XmlElement("Priority")]
		public string Priority { get; set; }
		[XmlElement("Title")]
		public string Title { get; set; }
		[XmlElement("Description")]
		public string Description { get; set; }

	}
}
