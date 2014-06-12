using System;

namespace AllureCSharpCommons.Attributes
{
    public class AllureTitleAttribute : Attribute
    {
        public AllureTitleAttribute(string value)
        {
            Value = value;
        }

        public string Value { get; private set; }
    }
}