using AllureCSharpCommons.AllureModel;

namespace AllureCSharpCommons.Events
{
	public interface ITestSuiteEvent : IEvent<testsuiteresult>
	{
		string Uid { get; }
	}
}

