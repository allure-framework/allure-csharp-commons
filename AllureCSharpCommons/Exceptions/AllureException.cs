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
