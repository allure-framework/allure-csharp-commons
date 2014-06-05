using System;
using AllureCSharpCommons.AllureModel;
using AllureCSharpCommons.Events;

namespace AllureCSharpCommons.AbstractEvents
{
    public abstract class AbstractTestCaseStatusChangeEvent : ITestCaseEvent
    {
        public Exception Throwable { get; set; }

        public abstract void Process(testcaseresult context);
        public string SuiteUid { get; set; }
    }
}