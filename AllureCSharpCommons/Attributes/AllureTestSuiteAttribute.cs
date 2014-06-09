using System;
using AllureCSharpCommons.AllureModel;

namespace AllureCSharpCommons.Attributes
{
    public class AllureTestSuiteAttribute : Attribute
    {
        public string Title { get; set; }
        public description Description { get; set; }
    }
}
