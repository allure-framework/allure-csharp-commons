// Author: Ilya Murzinov, https://github.com/ilya-murzinov
// E-mail: murz42@gmail.com
// Project's website: https://github.com/ilya-murzinov/AllureCSharpCommons
// Date: 2014.06.05

using AllureCSharpCommons.AbstractEvents;
using AllureCSharpCommons.AllureModel;
using AllureCSharpCommons.Utils;

namespace AllureCSharpCommons.Events
{
    public class TestSuiteStartedEvent : AbstractTestSuiteStartedEvent
    {
        public TestSuiteStartedEvent(string uid, string name)
        {
            Uid = uid;
            Name = name;
        }

        public override void Process(testsuiteresult context)
        {
            context.version = AllureConfig.Version;
            context.start = AllureResultsUtils.TimeStamp;
            context.name = Name;
            context.title = Title;
            context.description = Description;
            context.labels = Labels;
        }
    }
}