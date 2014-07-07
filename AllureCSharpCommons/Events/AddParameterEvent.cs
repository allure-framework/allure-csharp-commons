// Author: Ilya Murzinov, https://github.com/ilya-murzinov
// E-mail: murz42@gmail.com
// Project's website: https://github.com/ilya-murzinov/AllureCSharpCommons
// Date: 2014.06.05

using AllureCSharpCommons.AbstractEvents;
using AllureCSharpCommons.AllureModel;
using AllureCSharpCommons.Utils;

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
            context.parameters = ArraysUtils.Add(context.parameters, new parameter
            {
                name = Name,
                value = Value,
                kind = parameterkind.environmentvariable
            });
        }
    }
}