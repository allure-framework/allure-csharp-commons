// Author: Ilya Murzinov, https://github.com/ilya-murzinov
// E-mail: murz42@gmail.com
// Project's website: https://github.com/ilya-murzinov/AllureCSharpCommons
// Date: 2014.06.05

using AllureCSharpCommons.AbstractEvents;
using AllureCSharpCommons.AllureModel;
using NUnit.Framework;

namespace AllureCSharpCommons.Events
{
    public class StepFailureEvent : AbstractStepFailureEvent
    {
        public override void Process(step context)
        {
            status status = (Throwable != null
                             && Throwable.GetType() == typeof (AssertionException))
                ? status.failed
                : status.broken;
            context.status = status;
        }
    }
}