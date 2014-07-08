using AllureCSharpCommons.AbstractEvents;
using AllureCSharpCommons.AllureModel;
using AllureCSharpCommons.Utils;

namespace AllureCSharpCommons.Events
{
    public class TestCaseFinishedEvent : AbstractTestCaseFinishedEvent
    {
        public override void Process(testcaseresult context)
        {
            context.stop = AllureResultsUtils.TimeStamp;
        }
    }
}