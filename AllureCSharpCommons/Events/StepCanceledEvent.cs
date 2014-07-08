using AllureCSharpCommons.AbstractEvents;
using AllureCSharpCommons.AllureModel;

namespace AllureCSharpCommons.Events
{
    public class StepCanceledEvent : AbstractStepCanceledEvent
    {
        public override void Process(step context)
        {
            context.status = status.canceled;
        }
    }
}