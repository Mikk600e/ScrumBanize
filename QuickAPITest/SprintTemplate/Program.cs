using QuickAPITest;
using QuickAPITest.Definitions;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace SprintTemplate
{
    class Program
    {
        static void Main(string[] args)
        {
            string scrumwiseUser = ConfigurationManager.AppSettings["scrumwiseUser"];
            string scrumwiseKey = ConfigurationManager.AppSettings["scrumwiseKey"];
            string scrumwiseAPI = ConfigurationManager.AppSettings["scrumwiseAPI"];
            string scrumwiseBacklogListID = ConfigurationManager.AppSettings["scrumwiseBacklogListID"];
            string scrumwiseProjectID = ConfigurationManager.AppSettings["scrumwiseProjectID"];
            string scrumwiseTeamID = ConfigurationManager.AppSettings["scrumwiseTeamID"];

            Scrumwise scrumwiseConnection = new Scrumwise(scrumwiseProjectID, scrumwiseUser, scrumwiseKey, scrumwiseAPI, scrumwiseBacklogListID);
            ScrumwiseItemList scrumwiseItemList = scrumwiseConnection.GetListItemsInScrumwise();
            Console.WriteLine("Indtast venligst navn på Sprint templates skal tilknyttes: ");
            string sprintName = Console.ReadLine();
            Sprint sprint = scrumwiseConnection.GetSprintInScrumwise(sprintName);
            if (scrumwiseConnection.CreateSprintTemplate(sprint, scrumwiseItemList))
            {
                Console.WriteLine("Alt gik godt");
            }

            Console.ReadKey();
        }
    }
}
