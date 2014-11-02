using AllureCSharpCommons.AbstractEvents;
using AllureCSharpCommons.AllureModel;
using AllureCSharpCommons.Utils;
using System;

namespace AllureCSharpCommons.Events
{
    public class TestSuiteFinishedEvent : AbstractTestSuiteFinishedEvent
    {
        private DateTime? mFinished;
        
        public TestSuiteFinishedEvent(string uid, DateTime finished)
            : this(uid)
        {
            mFinished = finished;
        }
        
        public TestSuiteFinishedEvent(string uid)
        {
            Uid = uid;
        }

        public override void Process(testsuiteresult context)
        {
            context.stop = mFinished.HasValue ? mFinished.Value.ToUnixEpochTime() : AllureResultsUtils.TimeStamp;
        }
    }
}