
using System;

namespace AllureCSharpCommons.Attributes
{
    public class AllureTitleAttribute : Attribute
    {
        public string Value { get; private set; }

        public AllureTitleAttribute(string value)
        {
            Value = value;
        }
    }
}
