using System;
using AllureCSharpCommons.AllureModel;

namespace AllureCSharpCommons.Events
{
	public interface ITestCaseEvent : IEvent<testcaseresult>
	{
	    String SuiteUid { get; set; }
	}
}