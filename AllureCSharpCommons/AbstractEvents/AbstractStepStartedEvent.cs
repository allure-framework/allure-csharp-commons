using AllureCSharpCommons.AllureModel;
using AllureCSharpCommons.Events;

namespace AllureCSharpCommons.AbstractEvents
{
    public abstract class AbstractStepStartedEvent : IStepEvent
    {
        public string Name { get; set; }

        public string Title { get; set; }

        public abstract void Process(step context);
    }
}
