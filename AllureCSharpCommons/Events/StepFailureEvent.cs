using AllureCSharpCommons.AbstractEvents;
using AllureCSharpCommons.AllureModel;

namespace AllureCSharpCommons.Events
{
	public class StepFailureEvent : AbstractStepFailureEvent
	{
		public StepFailureEvent ()
		{
		}

	    public override void Process(step context)
	    {
	    }
	}
}

