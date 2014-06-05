using AllureCSharpCommons.AbstractEvents;
using AllureCSharpCommons.AllureModel;

namespace AllureCSharpCommons.Events
{
	public class TestCaseFailureEvent : AbstractTestCaseStatusChangeEvent
	{
		public TestCaseFailureEvent ()
		{
		}

	    public override void Process(testcaseresult context)
	    {
	    }
	}
}

