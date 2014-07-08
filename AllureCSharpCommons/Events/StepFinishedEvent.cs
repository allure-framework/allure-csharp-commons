using AllureCSharpCommons.AbstractEvents;
using AllureCSharpCommons.AllureModel;
using AllureCSharpCommons.Utils;

namespace AllureCSharpCommons.Events
{
    public class StepFinishedEvent : AbstractStepFinishedEvent
    {
        public override void Process(step context)
        {
            context.stop = AllureResultsUtils.TimeStamp;
        }
    }
}