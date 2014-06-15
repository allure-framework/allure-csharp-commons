// Author: Ilya Murzinov, https://github.com/ilya-murzinov
// E-mail: murz42@gmail.com
// Project's website: https://github.com/ilya-murzinov/AllureCSharpCommons
// Date: 2014.06.10

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