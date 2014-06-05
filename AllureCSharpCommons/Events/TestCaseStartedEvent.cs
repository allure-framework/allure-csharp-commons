using AllureCSharpCommons.AbstractEvents;
using AllureCSharpCommons.AllureModel;

namespace AllureCSharpCommons.Events
{
	public class TestCaseStartedEvent : AbstractTestCaseStartedEvent
	{
		public TestCaseStartedEvent ()
		{
		}

	    public override void Process(testcaseresult context)
	    {
	    }
	}
}

