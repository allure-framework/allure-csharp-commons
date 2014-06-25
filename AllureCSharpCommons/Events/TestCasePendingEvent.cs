// Author: Ilya Murzinov, https://github.com/ilya-murzinov
// E-mail: murz42@gmail.com
// Project's website: https://github.com/ilya-murzinov/AllureCSharpCommons
// Date: 2014.06.05

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

        public override string StackTrace { get; set; }
    }
}