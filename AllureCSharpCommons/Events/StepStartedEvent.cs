using AllureCSharpCommons.AbstractEvents;
using AllureCSharpCommons.AllureModel;
using AllureCSharpCommons.Utils;

namespace AllureCSharpCommons.Events
{
    public class StepStartedEvent : AbstractStepStartedEvent
    {
        public StepStartedEvent(string name)
        {
            Name = name;
        }

        public override void Process(step context)
        {
            context.name = Name;
            context.status = status.passed;
            context.start = AllureResultsUtils.TimeStamp;
            context.title = Title;
        }
    }
}