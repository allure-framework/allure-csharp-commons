using System;

namespace AllureCSharpCommons.Exceptions
{
    public class AllureException : Exception
    {
        public AllureException(String message)
            : base(message)
        {
        }
    }
}
