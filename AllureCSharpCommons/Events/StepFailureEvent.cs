using AllureCSharpCommons.AbstractEvents;
using AllureCSharpCommons.AllureModel;

namespace AllureCSharpCommons.Events
{
    public class StepFailureEvent : AbstractStepFailureEvent
    {
        public override void Process(step context)
        {
            status status = (Throwable != null
				&& Throwable.GetType().ToString().Contains("Assert"))
                ? status.failed
                : status.broken;
            context.status = status;
        }
    }
}