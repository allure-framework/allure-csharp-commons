using System;
using AllureCSharpCommons;

namespace AllureCSharpCommons
{
	public interface ITestSuiteEvent : IEvent<testsuiteresult>
	{
		string Uid { get; }
	}
}

