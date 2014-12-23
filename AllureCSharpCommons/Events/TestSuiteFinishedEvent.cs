using AllureCSharpCommons.AbstractEvents;
using AllureCSharpCommons.AllureModel;
using AllureCSharpCommons.Utils;
using System;

namespace AllureCSharpCommons.Events
{
    public class TestSuiteFinishedEvent : AbstractTestSuiteFinishedEvent
    {
        private DateTime? _finished;
        
        public TestSuiteFinishedEvent(string uid, DateTime finished)
            : this(uid)
        {
            _finished = finished;
        }
        
        public TestSuiteFinishedEvent(string uid)
        {
            Uid = uid;
        }

        public override void Process(testsuiteresult context)
        {
            context.stop = _finished.HasValue ? _finished.Value.ToUnixEpochTime() : AllureResultsUtils.TimeStamp;
        }
    }
}