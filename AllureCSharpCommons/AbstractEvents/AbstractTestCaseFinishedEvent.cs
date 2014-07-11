using AllureCSharpCommons.AllureModel;
using AllureCSharpCommons.Events;

namespace AllureCSharpCommons.AbstractEvents
{
    public abstract class AbstractTestCaseFinishedEvent : ITestCaseEvent
    {
        public abstract void Process(testcaseresult context);
        public string SuiteUid { get; set; }
    }
}
