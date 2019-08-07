using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using QuickAPITest.Definitions;
using RestSharp;
using RestSharp.Authenticators;
using RestSharp.Serialization;
using RestSharp.Deserializers;
using System.Xml;
using System.Xml.Linq;
namespace QuickAPITest
{
	class Program
	{
		static void Main(string[] args)
		{
			Kanbanize kanbanizeTasks = new Kanbanize("9", "til udvikler");
			Scrumwise scrumwiseConnection = new Scrumwise("niels@freeway.dk", "591D5EAF2868B8B0531D9B0BF03A017AE3F87BFFDF6A4919FC375CB6229B7351", "https://api.scrumwise.com/service/api/v1/");

        }
	}
}