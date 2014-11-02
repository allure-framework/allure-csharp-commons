using AllureCSharpCommons.AbstractEvents;
using AllureCSharpCommons.AllureModel;
using AllureCSharpCommons.Utils;
using System;

namespace AllureCSharpCommons.Events
{
    public class StepStartedEvent : AbstractStepStartedEvent
    {
        private DateTime? mStarted;
        
        public StepStartedEvent(string name, DateTime started)
            : this(name)
        {
            mStarted = started;
        }
        
        public StepStartedEvent(string name)
        {
            Name = name;
        }

        public override void Process(step context)
        {
            context.name = Name;
            context.status = status.passed;
            context.start = mStarted.HasValue ? mStarted.Value.ToUnixEpochTime() : AllureResultsUtils.TimeStamp;
            context.title = Title;
        }
    }
}