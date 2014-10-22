using System;

namespace AllureCSharpCommons
{
    [AttributeUsage(AttributeTargets.Method)]
    public class AllureAttachmentAttribute : Attribute
    {
        public AllureAttachmentAttribute() { }
    }
}

