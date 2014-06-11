using System;

namespace AllureCSharpCommons.Attributes
{
    public class AllureSeverityAttribute : Attribute
    {
        public AllureSeverityAttribute(string value)
        {
            Value = value;
        }

        public string Value { get; private set; }
    }
}