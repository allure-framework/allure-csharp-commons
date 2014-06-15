// Author: Ilya Murzinov, https://github.com/ilya-murzinov
// E-mail: murz42@gmail.com
// Project's website: https://github.com/ilya-murzinov/AllureCSharpCommons
// Date: 2014.06.05

using AllureCSharpCommons.AllureModel;

namespace AllureCSharpCommons.Events
{
    public interface ITestSuiteEvent : IEvent<testsuiteresult>
    {
        string Uid { get; }
    }
}