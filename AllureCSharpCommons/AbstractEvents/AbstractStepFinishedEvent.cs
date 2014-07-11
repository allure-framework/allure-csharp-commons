using AllureCSharpCommons.AllureModel;
using AllureCSharpCommons.Events;

namespace AllureCSharpCommons.AbstractEvents
{
    public abstract class AbstractStepFinishedEvent : IStepEvent
    {
        public abstract void Process(step context);
    }
}
