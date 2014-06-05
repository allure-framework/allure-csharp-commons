using AllureCSharpCommons.AbstractEvents;
using AllureCSharpCommons.AllureModel;

namespace AllureCSharpCommons.Events
{
	public class StepCanceledEvent : AbstractStepCanceledEvent
	{
		public StepCanceledEvent ()
		{
		}

	    public override void Process(step context)
	    {
	    }
	}
}

