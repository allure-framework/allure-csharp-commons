using System;
using AllureCSharpCommons.AbstractEvents;
using AllureCSharpCommons.AllureModel;
using AllureCSharpCommons.Utils;

namespace AllureCSharpCommons.Events
{
    public class MakeAttachEvent : AbstractMakeAttachEvent
    {
        public MakeAttachEvent(byte[] attachment, string title, string type)
        {
            Title = title;
            AttachmentType = type;
            Attach = AllureResultsUtils.WriteAttachmentSafely(attachment, title, type);
        }

        public override void Process(step context)
        {
            context.attachments.Add(Attach);
        }
    }
}