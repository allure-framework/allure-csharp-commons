// Author: Ilya Murzinov, https://github.com/ilya-murzinov
// E-mail: murz42@gmail.com
// Project's website: https://github.com/ilya-murzinov/AllureCSharpCommons
// Date: 2014.06.05

using System;
using AllureCSharpCommons.AllureModel;

namespace AllureCSharpCommons.Events
{
    public class TestCaseCanceledEvent : TestCaseStatusChangedEvent
    {
        protected override status Status
        {
            get { return status.canceled; }
        }

        protected override string Message
        {
            get { return "Test skipped with unknown reason"; }
            set { throw new InvalidOperationException("Don't set message here"); }
        }
    }
}