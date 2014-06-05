using System;
using AllureCSharpCommons.AbstractEvents;
using AllureCSharpCommons.AllureModel;

namespace AllureCSharpCommons.Events
{
	public class TestCaseStartedEvent : AbstractTestCaseStartedEvent
	{
		public TestCaseStartedEvent (string suiteUid, string name)
		{
		    Name = name;
		    SuiteUid = suiteUid;
		}

	    public override void Process(testcaseresult context)
	    {
	    }
	}
}

