using System.ComponentModel;

namespace QuickAPITest.Definitions
{
    public enum ScrumwisePriority
    {
		[Description("Lille")]
		Low = 1,
        [Description("Normal")]
        Medium = 2,
        [Description("Høj")]
        High = 3,
        [Description("Kritisk")]
        Urgent = 4
    }
}
