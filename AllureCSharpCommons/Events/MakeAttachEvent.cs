// Author: Ilya Murzinov, https://github.com/ilya-murzinov
// E-mail: murz42@gmail.com
// Project's website: https://github.com/ilya-murzinov/AllureCSharpCommons
// Date: 2014.06.05

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
            context.attachments = ArraysUtils.Add(context.attachments, Attach);
        }
    }
}