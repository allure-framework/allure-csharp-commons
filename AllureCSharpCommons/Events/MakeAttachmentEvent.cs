using AllureCSharpCommons.AbstractEvents;
using AllureCSharpCommons.AllureModel;
using AllureCSharpCommons.Utils;

namespace AllureCSharpCommons.Events
{
    public class MakeAttachmentEvent : AbstractMakeAttachmentEvent
    {
        /// <summary>
        ///     Writes attachments presented by byte array.
        /// </summary>
        /// <param name="attachment">Byte array presenting attachment</param>
        /// <param name="title">Title for internal use</param>
        /// <param name="type">Has to be valid MIME type of attachment</param>
        /// <returns></returns>
        public MakeAttachmentEvent(byte[] attachment, string title, string type)
        {
            Title = title;
            Type = type;
            Attachment = attachment;
        }

        public override void Process(testcaseresult context)
        {
            var attachment = AllureResultsUtils.WriteAttachmentSafely(Attachment, Title, Type);
            context.attachments = ArraysUtils.Add(context.attachments, attachment);
        }
    }
}