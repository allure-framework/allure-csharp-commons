// Author: Ilya Murzinov, https://github.com/ilya-murzinov
// E-mail: murz42@gmail.com
// Project's website: https://github.com/ilya-murzinov/AllureCSharpCommons
// Date: 2014.06.04

using AllureCSharpCommons.AllureModel;
using AllureCSharpCommons.Events;

namespace AllureCSharpCommons.AbstractEvents
{
    public abstract class AbstractTestSuiteFinishedEvent : ITestSuiteEvent
    {
        public string SuiteUid { get; set; }
        public string Uid { get; set; }

        public abstract void Process(testsuiteresult context);
    }
}