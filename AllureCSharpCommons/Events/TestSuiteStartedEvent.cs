using AllureCSharpCommons.AbstractEvents;
using AllureCSharpCommons.AllureModel;

namespace AllureCSharpCommons.Events
{
    public class TestSuiteStartedEvent : AbstractTestSuiteStartedEvent
    {
        public TestSuiteStartedEvent(string uid, string name)
        {
            Uid = uid;
            Name = name;
        }

        public override void Process(testsuiteresult context)
        {
        }
    }
}
