using AllureCSharpCommons.AbstractEvents;
using AllureCSharpCommons.AllureModel;

namespace AllureCSharpCommons.Events
{
    public class AddParameterEvent : AbstractTestCaseAddParameterEvent
    {
        public AddParameterEvent(string name, string value)
        {
            Name = name;
            Value = value;
        }

        public override void Process(testcaseresult context)
        {
            context.parameters.Add(new parameter()
            {
                name = Name,
                value = Value,
                kind = parameterkind.environmentvariable
            });
        }
    }
}