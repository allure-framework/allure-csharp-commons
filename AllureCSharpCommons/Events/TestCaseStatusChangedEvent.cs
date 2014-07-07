// Author: Ilya Murzinov, https://github.com/ilya-murzinov
// E-mail: murz42@gmail.com
// Project's website: https://github.com/ilya-murzinov/AllureCSharpCommons
// Date: 2014.06.05

using System;
using AllureCSharpCommons.AbstractEvents;
using AllureCSharpCommons.AllureModel;

namespace AllureCSharpCommons.Events
{
    public abstract class TestCaseStatusChangedEvent : AbstractTestCaseStatusChangeEvent
    {
        protected abstract status Status { get; }

        protected abstract string Message { get; set; }

        public abstract string StackTrace { get; set; }

        public override void Process(testcaseresult context)
        {
            context.status = Status;
            if (Throwable != null)
                context.failure = !String.IsNullOrEmpty(Throwable.StackTrace)
                    ? new failure
                    {
                        message = Throwable.Message,
                        stacktrace = Throwable.StackTrace
                    }
                    : new failure
                    {
                        message = Throwable.Message,
                        stacktrace = !String.IsNullOrEmpty(StackTrace)
                            ? StackTrace
                            : "There is no stack trace"
                    };
            else
            {
                context.failure = new failure
                {
                    message = Message
                };
            }
        }
    }
}