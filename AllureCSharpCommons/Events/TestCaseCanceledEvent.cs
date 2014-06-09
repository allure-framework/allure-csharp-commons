using AllureCSharpCommons.AbstractEvents;
using AllureCSharpCommons.AllureModel;

namespace AllureCSharpCommons.Events
{
    public class TestCaseCanceledEvent : AbstractTestCaseStatusChangeEvent
    {
        public override void Process(testcaseresult context)
        {
        }
    }
}