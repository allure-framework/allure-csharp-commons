// Author: Ilya Murzinov, https://github.com/ilya-murzinov
// E-mail: murz42@gmail.com
// Project's website: https://github.com/ilya-murzinov/AllureCSharpCommons
// Date: 2014.06.05

using AllureCSharpCommons.AbstractEvents;
using AllureCSharpCommons.AllureModel;
using AllureCSharpCommons.Utils;

namespace AllureCSharpCommons.Events
{
    public class TestCaseFinishedEvent : AbstractTestCaseFinishedEvent
    {
        public override void Process(testcaseresult context)
        {
            context.stop = AllureResultsUtils.TimeStamp;
        }
    }
}