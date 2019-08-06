using System.ComponentModel;

namespace QuickAPITest.Definitions
{
    public enum ScrumwisePriority
    {
        [Description("Normal")]
        Normal = 1,
        [Description("Høj")]
        High = 2,
        [Description("Kritisk")]
        Urgent = 3
    }
}
