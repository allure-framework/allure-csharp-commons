using AllureCSharpCommons.AbstractEvents;
using AllureCSharpCommons.AllureModel;
using AllureCSharpCommons.Utils;
using System;

namespace AllureCSharpCommons.Events
{
    public class StepFinishedEvent : AbstractStepFinishedEvent
    {
        private DateTime? mFinished;

        public StepFinishedEvent(DateTime finished)
        {
            mFinished = finished;
        }
        
        public StepFinishedEvent() { }
        
        public override void Process(step context)
        {
            context.stop = mFinished.HasValue ? mFinished.Value.ToUnixEpochTime() : AllureResultsUtils.TimeStamp;
        }
    }
}