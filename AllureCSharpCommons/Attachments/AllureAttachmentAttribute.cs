using System;

namespace AllureCSharpCommons
{
    [AttributeUsage(AttributeTargets.Method)]
    public sealed class AllureAttachmentAttribute : Attribute
    {
        public AllureAttachmentAttribute() { }
    }
}

