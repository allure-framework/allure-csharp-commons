using System;

namespace AllureCSharpCommons
{
    public class AttachmentAddedEventArgs : EventArgs
    {
        public AttachmentAddedEventArgs(object attachment, object context)
        {
            Attachment = attachment;
            Context = context;
        }

        public object Attachment { get; private set; }
        public object Context { get; private set; }
    }
}
