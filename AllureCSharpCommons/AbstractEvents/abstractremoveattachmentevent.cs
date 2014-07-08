using AllureCSharpCommons.AllureModel;
using AllureCSharpCommons.Events;

namespace AllureCSharpCommons.AbstractEvents
{
    public abstract class AbstractRemoveAttachmentEvent : IStepEvent
    {
        public Pattern Pattern { get; set; }

        public abstract void Process(step context);
    }
}