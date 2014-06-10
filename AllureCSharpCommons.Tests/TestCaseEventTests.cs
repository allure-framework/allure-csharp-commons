using System;
using AllureCSharpCommons.AllureModel;
using AllureCSharpCommons.Events;
using NUnit.Framework;

namespace AllureCSharpCommons.Tests
{
    [TestFixture]
    public class TestCaseEventTests
    {
        [Test]
        public void TestCaseStartedEventTest()
        {
            Allure lifecycle = Allure.DefaultLifecycle;
            TestSuiteStartedEvent tsevt = new TestSuiteStartedEvent("42", "suite42");
            lifecycle.Fire(tsevt);
            TestCaseStartedEvent evt = new TestCaseStartedEvent("42", "test name");
            lifecycle.Fire(evt);
            Assert.AreEqual(lifecycle.TestCaseStorage.IsValueCreated, true);
            Assert.AreEqual(lifecycle.TestCaseStorage.Get().name, "test name");
            Assert.AreEqual(lifecycle.TestCaseStorage.Get().status, status.passed);
            Assert.IsNull(lifecycle.TestCaseStorage.Get().steps);
            Assert.AreNotEqual(lifecycle.TestCaseStorage.Get().start, 0);
            Assert.AreEqual(lifecycle.TestCaseStorage.Get().stop, 0);
        }

        [Test]
        public void TestCaseCanceledEventTest()
        {
            Allure lifecycle = Allure.DefaultLifecycle;
            TestSuiteStartedEvent tsevt = new TestSuiteStartedEvent("42", "suite42");
            lifecycle.Fire(tsevt);
            TestCaseStartedEvent tcsevt = new TestCaseStartedEvent("42", "test name");
            lifecycle.Fire(tcsevt);
            TestCaseCanceledEvent evt = new TestCaseCanceledEvent();
            lifecycle.Fire(evt);
            Assert.AreEqual(lifecycle.TestCaseStorage.Get().status, status.canceled);
            Assert.AreEqual(lifecycle.TestCaseStorage.Get().failure.message, "Test skipped with unknown reason");
        }

        [Test]
        public void TestCasePendingEventWithoutMessageWithoutExceptionTest()
        {
            Allure lifecycle = Allure.DefaultLifecycle;
            TestSuiteStartedEvent tsevt = new TestSuiteStartedEvent("42", "suite42");
            lifecycle.Fire(tsevt);
            TestCaseStartedEvent tcsevt = new TestCaseStartedEvent("42", "test name");
            lifecycle.Fire(tcsevt);
            TestCasePendingEvent evt = new TestCasePendingEvent();
            lifecycle.Fire(evt);
            Assert.AreEqual(lifecycle.TestCaseStorage.Get().status, status.pending);
            Assert.AreEqual(lifecycle.TestCaseStorage.Get().failure.message, "Test not implemented yet");
            Assert.IsNull(lifecycle.TestCaseStorage.Get().failure.stacktrace);
        }

        [Test]
        public void TestCasePendingEventWithoutMessageWithExceptionTest()
        {
            Allure lifecycle = Allure.DefaultLifecycle;
            TestSuiteStartedEvent tsevt = new TestSuiteStartedEvent("42", "suite42");
            lifecycle.Fire(tsevt);
            TestCaseStartedEvent tcsevt = new TestCaseStartedEvent("42", "test name");
            lifecycle.Fire(tcsevt);
            TestCasePendingEvent evt = new TestCasePendingEvent()
                .WithThrowable(new Exception("exception"));
            lifecycle.Fire(evt);
            Assert.AreEqual(lifecycle.TestCaseStorage.Get().status, status.pending);
            Assert.AreEqual(lifecycle.TestCaseStorage.Get().failure.message, "exception");
            Assert.AreEqual(lifecycle.TestCaseStorage.Get().failure.stacktrace, "There is no stack trace");
        }

        [Test]
        public void TestCaseFailureWithAssertionExceptionEventTest()
        {
            Allure lifecycle = Allure.DefaultLifecycle;
            TestSuiteStartedEvent tsevt = new TestSuiteStartedEvent("42", "suite42");
            lifecycle.Fire(tsevt);
            TestCaseStartedEvent tcsevt = new TestCaseStartedEvent("42", "test name");
            lifecycle.Fire(tcsevt);
            TestCaseFailureEvent evt = new TestCaseFailureEvent()
                .WithThrowable(new AssertionException("assertion exception"));
            lifecycle.Fire(evt);
            Assert.AreEqual(lifecycle.TestCaseStorage.Get().status, status.failed);
            Assert.AreEqual(lifecycle.TestCaseStorage.Get().failure.message, "assertion exception");
        }

        [Test]
        public void TestCaseFailureWithOtherExceptionEventTest()
        {
            Allure lifecycle = Allure.DefaultLifecycle;
            TestSuiteStartedEvent tsevt = new TestSuiteStartedEvent("42", "suite42");
            lifecycle.Fire(tsevt);
            TestCaseStartedEvent tcsevt = new TestCaseStartedEvent("42", "test name");
            lifecycle.Fire(tcsevt);
            TestCaseFailureEvent evt = new TestCaseFailureEvent()
                .WithThrowable(new NullReferenceException("null reference exception"));
            lifecycle.Fire(evt);
            Assert.AreEqual(lifecycle.TestCaseStorage.Get().status, status.broken);
            Assert.AreEqual(lifecycle.TestCaseStorage.Get().failure.message, "null reference exception");
        }

        [Test]
        public void TestCaseFailureWithoutExceptionEventTest()
        {
            Allure lifecycle = Allure.DefaultLifecycle;
            TestSuiteStartedEvent tsevt = new TestSuiteStartedEvent("42", "suite42");
            lifecycle.Fire(tsevt);
            TestCaseStartedEvent tcsevt = new TestCaseStartedEvent("42", "test name");
            lifecycle.Fire(tcsevt);
            TestCaseFailureEvent evt = new TestCaseFailureEvent();
            lifecycle.Fire(evt);
            Assert.AreEqual(lifecycle.TestCaseStorage.Get().status, status.broken);
            Assert.AreEqual(lifecycle.TestCaseStorage.Get().failure.message, "Test broken with unknown reason");
        }

        [Test]
        public void TestCaseFinishedEventTest()
        {
            Allure lifecycle = Allure.DefaultLifecycle;
            TestSuiteStartedEvent tsevt = new TestSuiteStartedEvent("42", "suite42");
            lifecycle.Fire(tsevt);
            TestCaseStartedEvent tcsevt = new TestCaseStartedEvent("42", "test name");
            lifecycle.Fire(tcsevt);
            TestCaseFinishedEvent evt = new TestCaseFinishedEvent();
            lifecycle.Fire(evt);
            Assert.IsNull(lifecycle.TestCaseStorage.Value);
            Assert.AreEqual(lifecycle.TestSuiteStorage.Get("42").testcases.Length, 1);
            Assert.AreEqual(lifecycle.TestSuiteStorage.Get("42").testcases[0].name, "test name");
            Assert.AreEqual(lifecycle.TestSuiteStorage.Get("42").testcases[0].failure, null);
            Assert.AreEqual(lifecycle.TestSuiteStorage.Get("42").testcases[0].attachments, null);
            Assert.AreEqual(lifecycle.TestSuiteStorage.Get("42").testcases[0].steps, null);
            Assert.AreNotEqual(lifecycle.TestSuiteStorage.Get("42").testcases[0].stop, 0);
        }
    }
}
