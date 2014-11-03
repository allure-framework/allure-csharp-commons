using AllureCSharpCommons.AbstractEvents;
using AllureCSharpCommons.AllureModel;
using AllureCSharpCommons.Utils;
using System;

namespace AllureCSharpCommons.Events
{
    public class StepStartedEvent : AbstractStepStartedEvent
    {
        private DateTime? _started;
        
        public StepStartedEvent(string name, DateTime started)
            : this(name)
        {
            _started = started;
        }
        
        public StepStartedEvent(string name)
        {
            Name = name;
        }

        public override void Process(step context)
        {
            context.name = Name;
            context.status = status.passed;
            context.start = _started.HasValue ? _started.Value.ToUnixEpochTime() : AllureResultsUtils.TimeStamp;
            context.title = Title;
        }
    }
}