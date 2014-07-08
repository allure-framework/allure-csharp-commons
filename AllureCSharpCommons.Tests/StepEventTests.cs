using System;
using AllureCSharpCommons.AllureModel;
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
        public void MultipleStepsTest()
        {
            _lifecycle = Allure.DefaultLifecycle;
            var tsevt = new TestSuiteStartedEvent(SuiteUid, "suite42");
            _lifecycle.Fire(tsevt);
            var tcsevt = new TestCaseStartedEvent(SuiteUid, "test name");
            _lifecycle.Fire(tcsevt);
            _lifecycle.Fire(new StepStartedEvent("step1"));
            _lifecycle.Fire(new StepFinishedEvent());

            Assert.AreEqual(1, _lifecycle.StepStorage.Get().Last.Value.steps.Length);
            Assert.AreEqual("step1", _lifecycle.StepStorage.Get().Last.Value.steps[0].name);
            Assert.AreEqual(status.passed, _lifecycle.StepStorage.Get().Last.Value.steps[0].status);
            Assert.AreNotEqual(0, _lifecycle.StepStorage.Get().Last.Value.steps[0].stop);

            _lifecycle.Fire(new StepStartedEvent("step2"));
            _lifecycle.Fire(new StepFailureEvent());
            _lifecycle.Fire(new StepFinishedEvent());

            Assert.AreEqual(2, _lifecycle.StepStorage.Get().Last.Value.steps.Length);
            Assert.AreEqual("step2", _lifecycle.StepStorage.Get().Last.Value.steps[1].name);
            Assert.AreEqual(status.broken, _lifecycle.StepStorage.Get().Last.Value.steps[1].status);
            Assert.AreNotEqual(0, _lifecycle.StepStorage.Get().Last.Value.steps[1].stop);

            _lifecycle.Fire(new StepStartedEvent("step3"));
            _lifecycle.Fire(new StepFinishedEvent());

            Assert.AreEqual(3, _lifecycle.StepStorage.Get().Last.Value.steps.Length);
            Assert.AreEqual("step3", _lifecycle.StepStorage.Get().Last.Value.steps[2].name);
            Assert.AreEqual(status.passed, _lifecycle.StepStorage.Get().Last.Value.steps[2].status);
            Assert.AreNotEqual(0, _lifecycle.StepStorage.Get().Last.Value.steps[2].stop);
        }

        [Test]
        public void StepFailureEventWithAssertExceptionTest()
        {
            _lifecycle = Allure.DefaultLifecycle;
            var tsevt = new TestSuiteStartedEvent(SuiteUid, "suite42");
            _lifecycle.Fire(tsevt);
            var tcsevt = new TestCaseStartedEvent(SuiteUid, "test name");
            _lifecycle.Fire(tcsevt);
            var ssevt = new StepStartedEvent("step1");
            _lifecycle.Fire(ssevt);
            var evt = new StepFailureEvent
            {
                Throwable = new AssertionException("assertion exception")
            };
            _lifecycle.Fire(evt);
            Assert.AreEqual(2, _lifecycle.StepStorage.Get().Count); //Root step + step1
            Assert.AreEqual(status.failed, _lifecycle.StepStorage.Get().Last.Value.status);
        }

        [Test]
        public void StepFailureEventWithOtherExceptionTest()
        {
            _lifecycle = Allure.DefaultLifecycle;
            var tsevt = new TestSuiteStartedEvent(SuiteUid, "suite42");
            _lifecycle.Fire(tsevt);
            var tcsevt = new TestCaseStartedEvent(SuiteUid, "test name");
            _lifecycle.Fire(tcsevt);
            var ssevt = new StepStartedEvent("step1");
            _lifecycle.Fire(ssevt);
            var evt = new StepFailureEvent
            {
                Throwable = new NullReferenceException("other exception")
            };
            _lifecycle.Fire(evt);
            Assert.AreEqual(2, _lifecycle.StepStorage.Get().Count); //Root step + step1
            Assert.AreEqual(status.broken, _lifecycle.StepStorage.Get().Last.Value.status);
        }

        [Test]
        public void StepFinishedEventAfterStepFailureEventTest()
        {
            _lifecycle = Allure.DefaultLifecycle;
            var tsevt = new TestSuiteStartedEvent(SuiteUid, "suite42");
            _lifecycle.Fire(tsevt);
            var tcsevt = new TestCaseStartedEvent(SuiteUid, "test name");
            _lifecycle.Fire(tcsevt);
            var ssevt = new StepStartedEvent("step1");
            _lifecycle.Fire(ssevt);
            var sfevt = new StepFailureEvent
            {
                Throwable = new NullReferenceException("other exception")
            };
            _lifecycle.Fire(sfevt);
            var evt = new StepFinishedEvent();
            _lifecycle.Fire(evt);
            Assert.AreEqual(1, _lifecycle.StepStorage.Get().Last.Value.steps.Length);
            Assert.AreEqual("step1", _lifecycle.StepStorage.Get().Last.Value.steps[0].name);
            Assert.AreEqual(status.broken, _lifecycle.StepStorage.Get().Last.Value.steps[0].status);
            Assert.AreNotEqual(0, _lifecycle.StepStorage.Get().Last.Value.steps[0].stop);
        }

        [Test]
        public void StepFinishedEventTest()
        {
            _lifecycle = Allure.DefaultLifecycle;
            var tsevt = new TestSuiteStartedEvent(SuiteUid, "suite42");
            _lifecycle.Fire(tsevt);
            var tcsevt = new TestCaseStartedEvent(SuiteUid, "test name");
            _lifecycle.Fire(tcsevt);
            var ssevt = new StepStartedEvent("step1");
            _lifecycle.Fire(ssevt);
            var evt = new StepFinishedEvent();
            _lifecycle.Fire(evt);
            Assert.AreEqual(1, _lifecycle.StepStorage.Get().Last.Value.steps.Length);
            Assert.AreEqual("step1", _lifecycle.StepStorage.Get().Last.Value.steps[0].name);
            Assert.AreEqual(status.passed, _lifecycle.StepStorage.Get().Last.Value.steps[0].status);
            Assert.AreNotEqual(0, _lifecycle.StepStorage.Get().Last.Value.steps[0].stop);
        }

        [Test]
        public void StepStartedEventTest()
        {
            _lifecycle = Allure.DefaultLifecycle;
            var tsevt = new TestSuiteStartedEvent(SuiteUid, "suite42");
            _lifecycle.Fire(tsevt);
            var tcsevt = new TestCaseStartedEvent(SuiteUid, "test name");
            _lifecycle.Fire(tcsevt);
            var evt = new StepStartedEvent("step1");
            _lifecycle.Fire(evt);
            Assert.AreEqual(2, _lifecycle.StepStorage.Get().Count); //Root step + step1
            Assert.AreEqual("step1", _lifecycle.StepStorage.Get().Last.Value.name);
            Assert.AreNotEqual(0, _lifecycle.StepStorage.Get().Last.Value.start);
            Assert.AreEqual(0, _lifecycle.StepStorage.Get().Last.Value.stop);
        }
    }
}