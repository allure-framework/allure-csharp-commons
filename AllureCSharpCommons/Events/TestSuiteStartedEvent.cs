using System.Linq;
using AllureCSharpCommons.AbstractEvents;
using AllureCSharpCommons.AllureModel;
using AllureCSharpCommons.Utils;

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
            context.start = AllureResultsUtils.TimeStamp;
            context.name = Name;
            context.title = Title;
            context.description = Description;
            context.labels = Labels.ToList();
        }
    }
}