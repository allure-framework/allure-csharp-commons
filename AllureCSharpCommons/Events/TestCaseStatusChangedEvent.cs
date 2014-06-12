using AllureCSharpCommons.AbstractEvents;
using AllureCSharpCommons.AllureModel;

namespace AllureCSharpCommons.Events
{
    public abstract class TestCaseStatusChangedEvent : AbstractTestCaseStatusChangeEvent
    {
        protected abstract status Status { get; }

        protected abstract string Message { get; set; }

        public override void Process(testcaseresult context)
        {
            context.status = Status;
            if (Throwable != null)
                context.failure = Throwable == null
                    ? new failure
                    {
                        message = Throwable.Message,
                        stacktrace = Throwable.StackTrace
                    }
                    : new failure
                    {
                        message = Throwable.Message,
                        stacktrace = "There is no stack trace"
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