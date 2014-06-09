
using System;
using AllureCSharpCommons.AllureModel;

namespace AllureCSharpCommons.Attributes
{
    public class AllureTestCaseAttribute : Attribute
    {
        public string Title { get; set; }
        public description Description { get; set; }
        public string Severity { get; set; }
    }
}
