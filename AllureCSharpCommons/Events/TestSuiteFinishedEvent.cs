using AllureCSharpCommons.AbstractEvents;
using AllureCSharpCommons.AllureModel;
using AllureCSharpCommons.Utils;

namespace AllureCSharpCommons.Events
{
    public class TestSuiteFinishedEvent : AbstractTestSuiteFinishedEvent
    {
        public TestSuiteFinishedEvent(string uid)
        {
            Uid = uid;
        }

        public override void Process(testsuiteresult context)
        {
            context.stop = AllureResultsUtils.TimeStamp;
        }
    }
}