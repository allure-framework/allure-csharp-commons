using AllureCSharpCommons.AllureModel;

namespace AllureCSharpCommons.Events
{
    public class TestSuiteFinishedEvent : ITestSuiteEvent
    {
        public void Process(testsuiteresult context)
        {
        }

        public string Uid { get; private set; }
    }
}
