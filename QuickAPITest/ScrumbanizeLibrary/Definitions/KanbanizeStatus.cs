using System.ComponentModel;


namespace QuickAPITest.Definitions
{
	public enum KanbanizeStatus
	{
		[Description("To Do")]
		planlagt = 1,
		[Description("In Progress")]
		igang = 2,
		[Description("Done")]
		done = 3,
		[Description("Sprint complete")]
		arkiv= 4,


	}
}
