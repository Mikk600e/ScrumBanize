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
		done_96 = 3,

	}
}
