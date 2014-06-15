// Author: Ilya Murzinov, https://github.com/ilya-murzinov
// E-mail: murz42@gmail.com
// Project's website: https://github.com/ilya-murzinov/AllureCSharpCommons
// Date: 2014.06.10

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