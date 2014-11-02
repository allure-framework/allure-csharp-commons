using AllureCSharpCommons.AbstractEvents;
using AllureCSharpCommons.AllureModel;
using AllureCSharpCommons.Utils;
using System;

namespace AllureCSharpCommons.Events
{
    public class TestCaseFinishedEvent : AbstractTestCaseFinishedEvent
    {
        private DateTime? mFinished;
        
        public TestCaseFinishedEvent(DateTime finished)
        {
            mFinished = finished;
        }
        
        public TestCaseFinishedEvent() { }
        
        public override void Process(testcaseresult context)
        {
            context.stop = mFinished.HasValue ? mFinished.Value.ToUnixEpochTime() : AllureResultsUtils.TimeStamp;
        }
    }
}