using AllureCSharpCommons.AbstractEvents;
using AllureCSharpCommons.AllureModel;

namespace AllureCSharpCommons.Events
{
	public class TestCasePendingEvent : AbstractTestCaseStatusChangeEvent
	{
		public TestCasePendingEvent ()
		{
		}

	    public override void Process(testcaseresult context)
	    {
	    }
	}
}

