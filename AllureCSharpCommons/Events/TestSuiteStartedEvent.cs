using AllureCSharpCommons.AllureModel;

namespace AllureCSharpCommons.Events
{
    public class TestSuiteStartedEvent : ITestSuiteEvent
    {
        public void Process(testsuiteresult context)
        {
        }

        public string Uid { get; private set; }
    }
}
