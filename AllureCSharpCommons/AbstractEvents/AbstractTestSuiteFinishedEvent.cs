using AllureCSharpCommons.AllureModel;
using AllureCSharpCommons.Events;

namespace AllureCSharpCommons.AbstractEvents
{
    public abstract class AbstractTestSuiteFinishedEvent : ITestSuiteEvent
    {
        public string SuiteUid { get; set; }
        public string Uid { get; set; }

        public abstract void Process(testsuiteresult context);
    }
}
