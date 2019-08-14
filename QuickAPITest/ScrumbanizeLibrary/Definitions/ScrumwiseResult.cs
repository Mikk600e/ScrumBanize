using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuickAPITest.Definitions
{
    public class Rootobject
    {
        public string objectType { get; set; }
        public int dataVersion { get; set; }
        public Result result { get; set; }
    }

    public class Result
    {
        public string objectType { get; set; }
        public object persons { get; set; }
        public object deletedPersons { get; set; }
        public Project[] projects { get; set; }
    }

    public class Project
    {
        public string objectType { get; set; }
        public string id { get; set; }
        public object externalID { get; set; }
        public string name { get; set; }
        public string description { get; set; }
        public object link { get; set; }
        public string status { get; set; }
        public string roughEstimateUnit { get; set; }
        public string detailedEstimateUnit { get; set; }
        public string timeTrackingUnit { get; set; }
        public object checklists { get; set; }
        public object comments { get; set; }
        public object attachments { get; set; }
        public object tags { get; set; }
        public string[] productOwnerIDs { get; set; }
        public string[] stakeholderIDs { get; set; }
        public object teams { get; set; }
        public object messages { get; set; }
        public object backlogs { get; set; }
        public Backlogitem[] backlogItems { get; set; }
        public object releases { get; set; }
        public Sprint[] sprints { get; set; }
        public object boards { get; set; }
        public object files { get; set; }
        public object relationships { get; set; }
    }

    public class Backlogitem
    {
        public string objectType { get; set; }
        public string id { get; set; }
        public object externalID { get; set; }
        public int itemNumber { get; set; }
        public string projectID { get; set; }
        public string parentEpicID { get; set; }
        public string backlogListID { get; set; }
        public string name { get; set; }
        public string description { get; set; }
        public object link { get; set; }
        public string priority { get; set; }
        public string type { get; set; }
        public string color { get; set; }
        public string creatorID { get; set; }
        public long? creationDate { get; set; }
        public string bugState { get; set; }
        public string reproducible { get; set; }
        public string resolution { get; set; }
        public string releaseID { get; set; }
        public string status { get; set; }
        public long? toDoDate { get; set; }
        public long? inProgressDate { get; set; }
        public object toTestDate { get; set; }
        public long? doneDate { get; set; }
        public float roughEstimate { get; set; }
        public float estimate { get; set; }
        public float usedTime { get; set; }
        public float remainingWork { get; set; }
        public string sprintID { get; set; }
        public string teamID { get; set; }
        public string boardID { get; set; }
        public string boardColumnID { get; set; }
        public string[] assignedPersonIDs { get; set; }
        public object dueDate { get; set; }
        public string[] tagIDs { get; set; }
        public object checklists { get; set; }
        public object comments { get; set; }
        public object attachments { get; set; }
        public object timeEntries { get; set; }
        public object commits { get; set; }
        public Task[] tasks { get; set; }
        public Childbacklogitem[] childBacklogItems { get; set; }
    }

    public class Task
    {
        public string objectType { get; set; }
        public string id { get; set; }
        public object externalID { get; set; }
        public int taskNumber { get; set; }
        public string projectID { get; set; }
        public string backlogItemID { get; set; }
        public string name { get; set; }
        public string description { get; set; }
        public object link { get; set; }
        public object color { get; set; }
        public long? creationDate { get; set; }
        public string status { get; set; }
        public long? toDoDate { get; set; }
        public long? inProgressDate { get; set; }
        public object toTestDate { get; set; }
        public long? doneDate { get; set; }
        public string boardColumnID { get; set; }
        public float estimate { get; set; }
        public float usedTime { get; set; }
        public float remainingWork { get; set; }
        public string[] assignedPersonIDs { get; set; }
        public object dueDate { get; set; }
        public string[] tagIDs { get; set; }
        public object checklists { get; set; }
        public object comments { get; set; }
        public object attachments { get; set; }
        public object timeEntries { get; set; }
        public object commits { get; set; }
    }

    public class Childbacklogitem
    {
        public string objectType { get; set; }
        public string id { get; set; }
        public object externalID { get; set; }
        public int itemNumber { get; set; }
        public string projectID { get; set; }
        public string parentEpicID { get; set; }
        public string backlogListID { get; set; }
        public string name { get; set; }
        public string description { get; set; }
        public object link { get; set; }
        public string priority { get; set; }
        public string type { get; set; }
        public object color { get; set; }
        public string creatorID { get; set; }
        public long? creationDate { get; set; }
        public object bugState { get; set; }
        public object reproducible { get; set; }
        public object resolution { get; set; }
        public object releaseID { get; set; }
        public string status { get; set; }
        public long? toDoDate { get; set; }
        public long? inProgressDate { get; set; }
        public object toTestDate { get; set; }
        public long? doneDate { get; set; }
        public float roughEstimate { get; set; }
        public float estimate { get; set; }
        public float usedTime { get; set; }
        public float remainingWork { get; set; }
        public string sprintID { get; set; }
        public string teamID { get; set; }
        public string boardID { get; set; }
        public string boardColumnID { get; set; }
        public string[] assignedPersonIDs { get; set; }
        public object dueDate { get; set; }
        public string[] tagIDs { get; set; }
        public object checklists { get; set; }
        public object comments { get; set; }
        public object attachments { get; set; }
        public object timeEntries { get; set; }
        public object commits { get; set; }
        public Task1[] tasks { get; set; }
        public Childbacklogitem1[] childBacklogItems { get; set; }
    }

    public class Task1
    {
        public string objectType { get; set; }
        public string id { get; set; }
        public object externalID { get; set; }
        public int taskNumber { get; set; }
        public string projectID { get; set; }
        public string backlogItemID { get; set; }
        public string name { get; set; }
        public string description { get; set; }
        public object link { get; set; }
        public object color { get; set; }
        public object creationDate { get; set; }
        public string status { get; set; }
        public object toDoDate { get; set; }
        public object inProgressDate { get; set; }
        public object toTestDate { get; set; }
        public object doneDate { get; set; }
        public string boardColumnID { get; set; }
        public float estimate { get; set; }
        public float usedTime { get; set; }
        public float remainingWork { get; set; }
        public string[] assignedPersonIDs { get; set; }
        public object dueDate { get; set; }
        public object[] tagIDs { get; set; }
        public object checklists { get; set; }
        public object comments { get; set; }
        public object attachments { get; set; }
        public object timeEntries { get; set; }
        public object commits { get; set; }
    }

    public class Childbacklogitem1
    {
        public string objectType { get; set; }
        public string id { get; set; }
        public object externalID { get; set; }
        public int itemNumber { get; set; }
        public string projectID { get; set; }
        public string parentEpicID { get; set; }
        public string backlogListID { get; set; }
        public string name { get; set; }
        public string description { get; set; }
        public object link { get; set; }
        public object priority { get; set; }
        public string type { get; set; }
        public object color { get; set; }
        public string creatorID { get; set; }
        public long? creationDate { get; set; }
        public object bugState { get; set; }
        public object reproducible { get; set; }
        public object resolution { get; set; }
        public object releaseID { get; set; }
        public string status { get; set; }
        public object toDoDate { get; set; }
        public object inProgressDate { get; set; }
        public object toTestDate { get; set; }
        public object doneDate { get; set; }
        public float roughEstimate { get; set; }
        public float estimate { get; set; }
        public float usedTime { get; set; }
        public float remainingWork { get; set; }
        public object sprintID { get; set; }
        public object teamID { get; set; }
        public object boardID { get; set; }
        public object boardColumnID { get; set; }
        public object[] assignedPersonIDs { get; set; }
        public object dueDate { get; set; }
        public object[] tagIDs { get; set; }
        public object checklists { get; set; }
        public object comments { get; set; }
        public object attachments { get; set; }
        public object timeEntries { get; set; }
        public object commits { get; set; }
        public object[] tasks { get; set; }
        public object[] childBacklogItems { get; set; }
    }
    
    public class Sprint
    {
        public string objectType { get; set; }
        public string id { get; set; }
        public object externalID { get; set; }
        public string projectID { get; set; }
        public string name { get; set; }
        public string description { get; set; }
        public object link { get; set; }
        public string startDate { get; set; }
        public string endDate { get; set; }
        public string boardID { get; set; }
        public string status { get; set; }
        public bool isArchived { get; set; }
        public object[] tagIDs { get; set; }
        public object checklists { get; set; }
        public object comments { get; set; }
        public object attachments { get; set; }
        public Teamsprintparticipation[] teamSprintParticipations { get; set; }
    }

    public class Teamsprintparticipation
    {
        public string objectType { get; set; }
        public string id { get; set; }
        public string projectID { get; set; }
        public string teamID { get; set; }
        public string sprintID { get; set; }
        public float availableTime { get; set; }
        public string[] assignedBacklogItemIDs { get; set; }
    }

}
