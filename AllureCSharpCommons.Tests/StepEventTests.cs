using AllureCSharpCommons.Events;
using NUnit.Framework;

namespace AllureCSharpCommons.Tests
{
    [TestFixture]
    public class StepEventTests
    {
        private Allure _lifecycle;
        private const string SuiteUid = "suiteUid";

        [Test]
        public void StepStartedEventTest()
        {
            _lifecycle = Allure.DefaultLifecycle;
            TestSuiteStartedEvent tsevt = new TestSuiteStartedEvent(SuiteUid, "suite42");
            _lifecycle.Fire(tsevt);
            TestCaseStartedEvent tcsevt = new TestCaseStartedEvent(SuiteUid, "test name");
            _lifecycle.Fire(tcsevt);
            StepStartedEvent evt = new StepStartedEvent("step1");
            _lifecycle.Fire(evt);
            Assert.AreEqual(_lifecycle.StepStorage.Get().Count, 2); //Root step + step1
            Assert.AreEqual(_lifecycle.StepStorage.Get().Last.Value.name, "step1");
        }
    }
}
