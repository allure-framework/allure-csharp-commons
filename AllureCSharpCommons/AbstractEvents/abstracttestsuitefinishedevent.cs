using AllureCSharpCommons.AllureModel;
using AllureCSharpCommons.Events;

namespace AllureCSharpCommons.AbstractEvents
{
    public abstract class AbstractTestSuiteFinishedEvent : ITestCaseEvent
    {
        public string Uid { get; set; }

        public abstract void Process(testcaseresult context);
        public string SuiteUid { get; set; }
    }
}