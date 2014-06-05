using AllureCSharpCommons.AbstractEvents;
using AllureCSharpCommons.AllureModel;

namespace AllureCSharpCommons.Events
{
	public class TestCaseFinishedEvent : AbstractTestCaseFinishedEvent
	{
		public TestCaseFinishedEvent ()
		{
		}

	    public override void Process(testcaseresult context)
	    {
	    }
	}
}

