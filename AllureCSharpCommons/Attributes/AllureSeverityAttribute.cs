using System;
using AllureCSharpCommons.AllureModel;

namespace AllureCSharpCommons.Attributes
{
    [AttributeUsage(AttributeTargets.Method)]
    public class AllureSeverityAttribute : Attribute
    {
        public AllureSeverityAttribute(severitylevel value)
        {
            Value = value.ToString();
        }

        public string Value { get; private set; }
    }
}