using AllureCSharpCommons.AbstractEvents;
using AllureCSharpCommons.AllureModel;
using AllureCSharpCommons.Utils;
using System;

namespace AllureCSharpCommons.Events
{
    public class TestCaseFinishedEvent : AbstractTestCaseFinishedEvent
    {
        private DateTime? _finished;
        
        public TestCaseFinishedEvent(DateTime finished)
        {
            _finished = finished;
        }
        
        public TestCaseFinishedEvent() { }
        
        public override void Process(testcaseresult context)
        {
            context.stop = _finished.HasValue ? _finished.Value.ToUnixEpochTime() : AllureResultsUtils.TimeStamp;
        }
    }
}