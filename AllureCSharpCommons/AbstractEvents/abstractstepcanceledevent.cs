using System;
using AllureCSharpCommons.AllureModel;
using AllureCSharpCommons.Events;

namespace AllureCSharpCommons.AbstractEvents
{
    public abstract class AbstractStepCanceledEvent : IStepEvent
    {
        public Exception Throwable { get; set; }

        public abstract void Process(step context);
    }
}