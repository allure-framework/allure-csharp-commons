// Author: Ilya Murzinov, https://github.com/ilya-murzinov
// E-mail: murz42@gmail.com
// Project's website: https://github.com/ilya-murzinov/AllureCSharpCommons
// Date: 2014.06.05

using System;

namespace AllureCSharpCommons.Exceptions
{
    public class AllureException : Exception
    {
        public AllureException(string message)
            : base(message)
        {
        }
    }
}