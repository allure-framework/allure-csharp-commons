using System;
using AllureCSharpCommons.AllureModel;

namespace AllureCSharpCommons.Events
{
    public class TestCasePendingEvent : TestCaseStatusChangedEvent
    {
        private string _message = "Test not implemented yet";

        protected override status Status
        {
            get { return status.pending; }
        }

        protected override string Message
        {
            get { return _message; }
            set { _message = value; }
        }

        public TestCasePendingEvent WithMessage(string message)
        {
            Message = message;
            return this;
        }

        public TestCasePendingEvent WithThrowable(Exception throwable)
        {
            Throwable = throwable;
            return this;
        }
    }
}