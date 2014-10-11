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
            {
                string message = Throwable.Message;
                string stacktrace = !String.IsNullOrEmpty(StackTrace) ? StackTrace : "There is no stack trace";
                
                context.failure = new failure(message, stacktrace);
            }
            else
            {
                context.failure = new failure(Message);
            }
        }
    }
}