using System.Linq;
using AllureCSharpCommons.AbstractEvents;
using AllureCSharpCommons.AllureModel;
using AllureCSharpCommons.Utils;

namespace AllureCSharpCommons.Events
{
    public class TestCaseStartedEvent : AbstractTestCaseStartedEvent
    {
        public TestCaseStartedEvent(string suiteUid, string name)
        {
            Name = name;
            SuiteUid = suiteUid;
        }

        public override void Process(testcaseresult context)
        {
            context.start = AllureResultsUtils.TimeStamp;
            context.status = status.passed;
            context.name = Name;
            context.title = Title;
            context.description = Description;
            context.labels = Labels;
        }
    }
}