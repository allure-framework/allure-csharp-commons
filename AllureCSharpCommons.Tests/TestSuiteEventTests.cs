using AllureCSharpCommons.Events;
using NUnit.Framework;

namespace AllureCSharpCommons.Tests
{
    [TestFixture]
    public class TestSuiteEventTests
    {
        [Test]
        public void SuiteStartedEventTest()
        {
            Allure lifecycle = Allure.DefaultLifecycle;
            TestSuiteStartedEvent evt = new TestSuiteStartedEvent("42", "suite42");
            lifecycle.Fire(evt);
            Assert.AreEqual(lifecycle.TestSuiteStorage.Map.Count, 1);
            Assert.True(lifecycle.TestSuiteStorage.Map.ContainsKey("42"));
            Assert.AreEqual(lifecycle.TestSuiteStorage.Get("42").testcases, null);
            Assert.AreEqual(lifecycle.TestSuiteStorage.Get("42").name, "suite42");
        }

        [Test]
        public void SuiteFinishedTest()
        {
            Allure lifecycle = Allure.DefaultLifecycle;
            TestSuiteStartedEvent tsevt = new TestSuiteStartedEvent("42", "suite42");
            lifecycle.Fire(tsevt);
            TestSuiteFinishedEvent tfevt = new TestSuiteFinishedEvent("42");
            lifecycle.Fire(tfevt);
            Assert.AreEqual(lifecycle.TestSuiteStorage.Map.Count, 0);
            Assert.False(lifecycle.TestSuiteStorage.Map.ContainsKey("42"));
        }
    }
}
