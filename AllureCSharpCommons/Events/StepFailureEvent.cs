using AllureCSharpCommons.AbstractEvents;
using AllureCSharpCommons.AllureModel;
using NUnit.Framework;

namespace AllureCSharpCommons.Events
{
    public class StepFailureEvent : AbstractStepFailureEvent
    {
        public override void Process(step context)
        {
            var status = (Throwable != null
                          && Throwable.GetType() == typeof (AssertionException))
                ? status.failed
                : status.broken;
            context.status = status;
        }
    }
}