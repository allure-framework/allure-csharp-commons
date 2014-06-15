// Author: Ilya Murzinov, https://github.com/ilya-murzinov
// E-mail: murz42@gmail.com
// Project's website: https://github.com/ilya-murzinov/AllureCSharpCommons
// Date: 2014.06.05

using AllureCSharpCommons.AbstractEvents;
using AllureCSharpCommons.AllureModel;
using AllureCSharpCommons.Utils;

namespace AllureCSharpCommons.Events
{
    public class TestSuiteFinishedEvent : AbstractTestSuiteFinishedEvent
    {
        public TestSuiteFinishedEvent(string uid)
        {
            Uid = uid;
        }

        public override void Process(testsuiteresult context)
        {
            context.stop = AllureResultsUtils.TimeStamp;
        }
    }
}