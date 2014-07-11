using System;

namespace AllureCSharpCommons.Attributes
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
    public class AllureTitleAttribute : Attribute
    {
        public AllureTitleAttribute(string value)
        {
            Value = value;
        }

        public string Value { get; private set; }
    }
}