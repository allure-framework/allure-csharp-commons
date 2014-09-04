using AllureCSharpCommons.AbstractEvents;
using AllureCSharpCommons.AllureModel;
using AllureCSharpCommons.Exceptions;

namespace AllureCSharpCommons.Events
{
    public class StepFailureEvent : AbstractStepFailureEvent
    {
        public override void Process(step context)
        {
            status status = (Throwable != null
                          && Throwable.GetType() == typeof (AssertionException))
                ? status.failed
                : status.broken;
            context.status = status;
        }
    }
}