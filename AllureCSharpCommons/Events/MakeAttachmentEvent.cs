using AllureCSharpCommons.AbstractEvents;
using AllureCSharpCommons.AllureModel;
using AllureCSharpCommons.Utils;

namespace AllureCSharpCommons.Events
{
    public class MakeAttachmentEvent : AbstractMakeAttachmentEvent
    {
        public MakeAttachmentEvent(byte[] attachment, string title, string type)
        {
            Title = title;
            Type = type;
            Attachment = attachment;
        }

        public override void Process(testcaseresult context)
        {
            attachment attachment = AllureResultsUtils.WriteAttachmentSafely(Attachment, Title, Type);
            context.attachments.Add(attachment);
        }
    }
}