
using System;

namespace AllureCSharpCommons.Attributes
{
    public class AllureSeverityAttribute : Attribute
    {
        public string Value { get; private set; }

        public AllureSeverityAttribute(string value)
        {
            Value = value;
        }
    }
}
