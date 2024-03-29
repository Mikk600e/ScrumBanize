﻿using QuickAPITest;
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
            string scrumwiseTemplateTagID = ConfigurationManager.AppSettings["scrumwiseTemplateTagID"];
            string scrumwiseEstimateUnit = ConfigurationManager.AppSettings["scrumwiseEstimateUnit"];

            Scrumwise scrumwiseConnection = new Scrumwise(scrumwiseProjectID, scrumwiseUser, scrumwiseKey, scrumwiseAPI, scrumwiseBacklogListID, scrumwiseTemplateTagID, scrumwiseTeamID, scrumwiseEstimateUnit);
            ScrumwiseItemList scrumwiseItemList = scrumwiseConnection.GetTemplateItemsInScrumwise();
            bool keepGoing = true;
            while (keepGoing)
            {
                Console.WriteLine("Indtast venligst navn på Sprint templates skal tilknyttes eller Q for at afslutte: ");
                string sprintName = Console.ReadLine();
                if(sprintName == "Q")
                {
                    keepGoing = false;
                }
                else
                {
                    Sprint sprint = scrumwiseConnection.GetSprintInScrumwise(sprintName);
                    if (sprint.id != null)
                    {
                        if (scrumwiseConnection.CreateSprintTemplate(sprint, scrumwiseItemList))
                        {
                            Console.WriteLine("Alt gik godt");
                            keepGoing = false;
                        }
                    }
                }
            }
            Console.ReadKey();
        }
    }
}
