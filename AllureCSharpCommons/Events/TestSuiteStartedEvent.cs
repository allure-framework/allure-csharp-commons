using AllureCSharpCommons.AbstractEvents;
using AllureCSharpCommons.AllureModel;
using AllureCSharpCommons.Utils;
using System;

namespace AllureCSharpCommons.Events
{
    public class TestSuiteStartedEvent : AbstractTestSuiteStartedEvent
    {
        private DateTime? mStarted;
        
        public TestSuiteStartedEvent(string uid, string name, DateTime started)
            : this(uid, name)
        {
            mStarted = started;
        }
        
        public TestSuiteStartedEvent(string uid, string name)
        {
            Uid = uid;
            Name = (name != null) ? name : string.Empty;
        }

        public override void Process(testsuiteresult context)
        {
            context.version = AllureConfig.Version;
            context.start = mStarted.HasValue ? mStarted.Value.ToUnixEpochTime() : AllureResultsUtils.TimeStamp;
            context.name = Name;
            context.title = Title;
            context.description = Description;
            context.labels = Labels;
        }
    }
}
