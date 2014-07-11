using AllureCSharpCommons.AllureModel;
using AllureCSharpCommons.Events;

namespace AllureCSharpCommons.AbstractEvents
{
    public abstract class AbstractTestCaseAddParameterEvent : ITestCaseEvent
    {
        public string Name { get; set; }

        public string Value { get; set; }

        public abstract void Process(testcaseresult context);
        public string SuiteUid { get; set; }
    }
}
