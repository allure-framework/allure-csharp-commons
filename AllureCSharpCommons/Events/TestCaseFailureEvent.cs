using System;
using AllureCSharpCommons.AllureModel;
using NUnit.Framework;

namespace AllureCSharpCommons.Events
{
    public class TestCaseFailureEvent : TestCaseStatusChangedEvent
    {
        protected override status Status
        {
            get
            {
                return (Throwable == null
                        || Throwable.GetType() != typeof (AssertionException))
                    ? status.broken
                    : status.failed;
            }
        }

        protected override string Message
        {
            get
            {
                return Status == status.failed
                    ? "Test failed with unknown reason"
                    : "Test broken with unknown reason";
            }
            set { throw new InvalidOperationException("Message"); }
        }
    }
}