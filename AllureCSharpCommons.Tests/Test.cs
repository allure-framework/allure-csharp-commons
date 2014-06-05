using AllureCSharpCommons.Events;
using NUnit.Framework;

namespace AllureCSharpCommons.Tests
{
    [TestFixture]
    public class Test
    {
        [Test]
        public void Test1()
        {
            Allure lifecycle = Allure.Lifecycle;
            lifecycle.Fire(new TestSuiteStartedEvent("1", "2"));
            lifecycle.Fire(new TestCaseStartedEvent("1", "2"));
            lifecycle.Fire(new StepStartedEvent());
            lifecycle.Fire(new StepFinishedEvent());
            lifecycle.Fire(new TestCaseFinishedEvent());
            lifecycle.Fire(new TestSuiteFinishedEvent("1"));
        }
    }
}
