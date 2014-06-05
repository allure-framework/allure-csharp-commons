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
    }
}