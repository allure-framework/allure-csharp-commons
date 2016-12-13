using System;

namespace AllureCSharpCommons
{
    public class AttachmentAddedEventArgs : EventArgs
    {
        public AttachmentAddedEventArgs(string mimeType, string title, object attachment, object context)
        {
            Attachment = attachment;
            Context = context;
        }
        
        public string MimeType { get; private set; }
        public string Title { get; private set; }
        public object Attachment { get; private set; }
        public object Context { get; private set; }
        
    }
}
