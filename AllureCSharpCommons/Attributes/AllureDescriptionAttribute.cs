using System;
using AllureCSharpCommons.AllureModel;

namespace AllureCSharpCommons.Attributes
{
    public class AllureDescriptionAttribute : Attribute
    {
        public AllureDescriptionAttribute(string value, descriptiontype type)
        {
            Value = new description
            {
                type = type,
                Value = value
            };
        }

        public description Value { get; private set; }
    }
}