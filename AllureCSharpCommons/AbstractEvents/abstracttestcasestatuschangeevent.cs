// Author: Ilya Murzinov, https://github.com/ilya-murzinov
// E-mail: murz42@gmail.com
// Project's website: https://github.com/ilya-murzinov/AllureCSharpCommons
// Date: 2014.06.05

using System;
using AllureCSharpCommons.AllureModel;
using AllureCSharpCommons.Events;

namespace AllureCSharpCommons.AbstractEvents
{
    public abstract class AbstractTestCaseStatusChangeEvent : ITestCaseEvent
    {
        public Exception Throwable { get; set; }

        public abstract void Process(testcaseresult context);

        public string SuiteUid { get; set; }
    }
}