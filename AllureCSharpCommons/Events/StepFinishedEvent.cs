using AllureCSharpCommons.AbstractEvents;
using AllureCSharpCommons.AllureModel;
using AllureCSharpCommons.Utils;
using System;

namespace AllureCSharpCommons.Events
{
    public class StepFinishedEvent : AbstractStepFinishedEvent
    {
        private DateTime? _finished;

        public StepFinishedEvent(DateTime finished)
        {
            _finished = finished;
        }
        
        public StepFinishedEvent() { }
        
        public override void Process(step context)
        {
            context.stop = _finished.HasValue ? _finished.Value.ToUnixEpochTime() : AllureResultsUtils.TimeStamp;
        }
    }
}