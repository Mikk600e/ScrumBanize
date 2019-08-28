using System.ComponentModel;

namespace QuickAPITest.Definitions
{
	public enum KanbannizeTypes
	{
		[Description("Set type as bug")]
		Bug = 1,
		[Description("Set type as Feature")]
		Feature = 2,
	}
}
