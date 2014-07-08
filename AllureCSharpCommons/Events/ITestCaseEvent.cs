using AllureCSharpCommons.AllureModel;

namespace AllureCSharpCommons.Events
{
    public interface ITestCaseEvent : IEvent<testcaseresult>
    {
        string SuiteUid { get; set; }
    }
}