using System;

namespace AllureCSharpCommons
{
    [AttributeUsage(AttributeTargets.Method, AllowMultiple = false)]
    public sealed class AllureAttachmentAttribute : Attribute
    {
        public AllureAttachmentAttribute(string mimeType)
            : this(mimeType, null) { }
        
        public AllureAttachmentAttribute(string mimeType, string title) 
        { 
            MimeType = mimeType;
            Title = title;
        }
        
        public string MimeType { get; private set; }
        
        public string Title { get; private set; }
    }
}

