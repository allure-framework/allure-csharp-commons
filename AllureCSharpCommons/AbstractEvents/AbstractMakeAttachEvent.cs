using AllureCSharpCommons.AllureModel;
using AllureCSharpCommons.Events;

namespace AllureCSharpCommons.AbstractEvents
{
    public abstract class AbstractMakeAttachEvent : IStepEvent
    {
        public string Title { get; set; }

        public string AttachmentType { get; set; }

        public attachment Attach { get; set; }

        public abstract void Process(step context);
    }
}
