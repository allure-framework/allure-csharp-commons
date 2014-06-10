using System;
using AllureCSharpCommons.AllureModel;
using AllureCSharpCommons.Events;
using NUnit.Framework;

namespace AllureCSharpCommons.Tests
{
    [TestFixture]
    public class TestCaseEventTests
    {
        private Allure _lifecycle;
        private const string SuiteUid = "suiteUid";

        [Test]
        public void TestCaseStartedEventTest()
        {
            _lifecycle = Allure.DefaultLifecycle;
            TestSuiteStartedEvent tsevt = new TestSuiteStartedEvent(SuiteUid, "suite42");
            _lifecycle.Fire(tsevt);
            TestCaseStartedEvent evt = new TestCaseStartedEvent(SuiteUid, "test name");
            _lifecycle.Fire(evt);
            Assert.AreEqual(_lifecycle.TestCaseStorage.IsValueCreated, true);
            Assert.AreEqual(_lifecycle.TestSuiteStorage.Get(SuiteUid).testcases[0].name, "test name");
            Assert.AreEqual(_lifecycle.TestSuiteStorage.Get(SuiteUid).testcases[0].status, status.passed);
            Assert.IsNull(_lifecycle.TestSuiteStorage.Get(SuiteUid).testcases[0].steps);
            Assert.AreNotEqual(_lifecycle.TestSuiteStorage.Get(SuiteUid).testcases[0].start, 0);
            Assert.AreEqual(_lifecycle.TestSuiteStorage.Get(SuiteUid).testcases[0].stop, 0);
        }

        [Test]
        public void TestCaseCanceledEventTest()
        {
            _lifecycle = Allure.DefaultLifecycle;
            TestSuiteStartedEvent tsevt = new TestSuiteStartedEvent(SuiteUid, "suite42");
            _lifecycle.Fire(tsevt);
            TestCaseStartedEvent tcsevt = new TestCaseStartedEvent(SuiteUid, "test name");
            _lifecycle.Fire(tcsevt);
            TestCaseCanceledEvent evt = new TestCaseCanceledEvent();
            _lifecycle.Fire(evt);
            Assert.AreEqual(_lifecycle.TestSuiteStorage.Get(SuiteUid).testcases[0].status, status.canceled);
            Assert.AreEqual(_lifecycle.TestSuiteStorage.Get(SuiteUid).testcases[0].failure.message, "Test skipped with unknown reason");
        }

        [Test]
        public void TestCasePendingEventWithoutMessageWithoutExceptionTest()
        {
            _lifecycle = Allure.DefaultLifecycle;
            TestSuiteStartedEvent tsevt = new TestSuiteStartedEvent(SuiteUid, "suite42");
            _lifecycle.Fire(tsevt);
            TestCaseStartedEvent tcsevt = new TestCaseStartedEvent(SuiteUid, "test name");
            _lifecycle.Fire(tcsevt);
            TestCasePendingEvent evt = new TestCasePendingEvent();
            _lifecycle.Fire(evt);
            Assert.AreEqual(_lifecycle.TestSuiteStorage.Get(SuiteUid).testcases[0].status, status.pending);
            Assert.AreEqual(_lifecycle.TestSuiteStorage.Get(SuiteUid).testcases[0].failure.message, "Test not implemented yet");
            Assert.IsNull(_lifecycle.TestSuiteStorage.Get(SuiteUid).testcases[0].failure.stacktrace);
        }

        [Test]
        public void TestCasePendingEventWithoutMessageWithExceptionTest()
        {
            _lifecycle = Allure.DefaultLifecycle;
            TestSuiteStartedEvent tsevt = new TestSuiteStartedEvent(SuiteUid, "suite42");
            _lifecycle.Fire(tsevt);
            TestCaseStartedEvent tcsevt = new TestCaseStartedEvent(SuiteUid, "test name");
            _lifecycle.Fire(tcsevt);
            TestCasePendingEvent evt = new TestCasePendingEvent()
                .WithThrowable(new Exception("exception"));
            _lifecycle.Fire(evt);
            Assert.AreEqual(_lifecycle.TestSuiteStorage.Get(SuiteUid).testcases[0].status, status.pending);
            Assert.AreEqual(_lifecycle.TestSuiteStorage.Get(SuiteUid).testcases[0].failure.message, "exception");
            Assert.AreEqual(_lifecycle.TestSuiteStorage.Get(SuiteUid).testcases[0].failure.stacktrace, "There is no stack trace");
        }

        [Test]
        public void TestCaseFailureWithAssertionExceptionEventTest()
        {
            _lifecycle = Allure.DefaultLifecycle;
            TestSuiteStartedEvent tsevt = new TestSuiteStartedEvent(SuiteUid, "suite42");
            _lifecycle.Fire(tsevt);
            TestCaseStartedEvent tcsevt = new TestCaseStartedEvent(SuiteUid, "test name");
            _lifecycle.Fire(tcsevt);
            TestCaseFailureEvent evt = new TestCaseFailureEvent()
                .WithThrowable(new AssertionException("assertion exception"));
            _lifecycle.Fire(evt);
            Assert.AreEqual(_lifecycle.TestSuiteStorage.Get(SuiteUid).testcases[0].status, status.failed);
            Assert.AreEqual(_lifecycle.TestSuiteStorage.Get(SuiteUid).testcases[0].failure.message, "assertion exception");
        }

        [Test]
        public void TestCaseFailureWithOtherExceptionEventTest()
        {
            _lifecycle = Allure.DefaultLifecycle;
            TestSuiteStartedEvent tsevt = new TestSuiteStartedEvent(SuiteUid, "suite42");
            _lifecycle.Fire(tsevt);
            TestCaseStartedEvent tcsevt = new TestCaseStartedEvent(SuiteUid, "test name");
            _lifecycle.Fire(tcsevt);
            TestCaseFailureEvent evt = new TestCaseFailureEvent()
                .WithThrowable(new NullReferenceException("null reference exception"));
            _lifecycle.Fire(evt);
            Assert.AreEqual(_lifecycle.TestSuiteStorage.Get(SuiteUid).testcases[0].status, status.broken);
            Assert.AreEqual(_lifecycle.TestSuiteStorage.Get(SuiteUid).testcases[0].failure.message, "null reference exception");
        }

        [Test]
        public void TestCaseFailureWithoutExceptionEventTest()
        {
            _lifecycle = Allure.DefaultLifecycle;
            TestSuiteStartedEvent tsevt = new TestSuiteStartedEvent(SuiteUid, "suite42");
            _lifecycle.Fire(tsevt);
            TestCaseStartedEvent tcsevt = new TestCaseStartedEvent(SuiteUid, "test name");
            _lifecycle.Fire(tcsevt);
            TestCaseFailureEvent evt = new TestCaseFailureEvent();
            _lifecycle.Fire(evt);
            Assert.AreEqual(_lifecycle.TestSuiteStorage.Get(SuiteUid).testcases[0].status, status.broken);
            Assert.AreEqual(_lifecycle.TestSuiteStorage.Get(SuiteUid).testcases[0].failure.message, "Test broken with unknown reason");
        }

        [Test]
        public void TestCaseFinishedEventTest()
        {
            _lifecycle = Allure.DefaultLifecycle;
            TestSuiteStartedEvent tsevt = new TestSuiteStartedEvent(SuiteUid, "suite42");
            _lifecycle.Fire(tsevt);
            TestCaseStartedEvent tcsevt = new TestCaseStartedEvent(SuiteUid, "test name");
            _lifecycle.Fire(tcsevt);
            TestCaseFinishedEvent evt = new TestCaseFinishedEvent();
            _lifecycle.Fire(evt);
            Assert.IsNull(_lifecycle.TestCaseStorage.Value);
            Assert.AreEqual(_lifecycle.TestSuiteStorage.Get(SuiteUid).testcases.Length, 1);
            Assert.AreEqual(_lifecycle.TestSuiteStorage.Get(SuiteUid).testcases[0].name, "test name");
            Assert.AreEqual(_lifecycle.TestSuiteStorage.Get(SuiteUid).testcases[0].failure, null);
            Assert.AreEqual(_lifecycle.TestSuiteStorage.Get(SuiteUid).testcases[0].attachments, null);
            Assert.AreEqual(_lifecycle.TestSuiteStorage.Get(SuiteUid).testcases[0].steps, null);
            Assert.AreNotEqual(_lifecycle.TestSuiteStorage.Get(SuiteUid).testcases[0].stop, 0);
        }

        [Test]
        public void TestCaseFinishedEventAfterTestCaseCancelledEventTest()
        {
            TestCaseCanceledEventTest();
            TestCaseFinishedEvent evt = new TestCaseFinishedEvent();
            _lifecycle.Fire(evt);
            Assert.IsNull(_lifecycle.TestCaseStorage.Value);
            Assert.AreEqual(_lifecycle.TestSuiteStorage.Get(SuiteUid).testcases.Length, 1);
            Assert.AreEqual(_lifecycle.TestSuiteStorage.Get(SuiteUid).testcases[0].name, "test name");
            Assert.AreEqual(_lifecycle.TestSuiteStorage.Get(SuiteUid).testcases[0].status, status.canceled);
            Assert.AreEqual(_lifecycle.TestSuiteStorage.Get(SuiteUid).testcases[0].failure.message, "Test skipped with unknown reason");
            Assert.AreEqual(_lifecycle.TestSuiteStorage.Get(SuiteUid).testcases[0].attachments, null);
            Assert.AreEqual(_lifecycle.TestSuiteStorage.Get(SuiteUid).testcases[0].steps, null);
            Assert.AreNotEqual(_lifecycle.TestSuiteStorage.Get(SuiteUid).testcases[0].stop, 0);
        }

        [Test]
        public void MultipleTestCasesTest()
        {
            _lifecycle = Allure.DefaultLifecycle;
            TestSuiteStartedEvent tsevt = new TestSuiteStartedEvent(SuiteUid, "suite42");
            _lifecycle.Fire(tsevt);

            TestCaseStartedEvent evt = new TestCaseStartedEvent(SuiteUid, "test name1");
            _lifecycle.Fire(evt);
            _lifecycle.Fire(new TestCaseCanceledEvent());
            _lifecycle.Fire(new TestCaseFinishedEvent());

            Assert.IsNull(_lifecycle.TestCaseStorage.Value);
            Assert.AreEqual(_lifecycle.TestSuiteStorage.Get(SuiteUid).testcases.Length, 1);
            Assert.AreEqual(_lifecycle.TestSuiteStorage.Get(SuiteUid).testcases[0].name, "test name1");
            Assert.AreEqual(_lifecycle.TestSuiteStorage.Get(SuiteUid).testcases[0].status, status.canceled);
            Assert.AreEqual(_lifecycle.TestSuiteStorage.Get(SuiteUid).testcases[0].failure.message, "Test skipped with unknown reason");
            Assert.AreEqual(_lifecycle.TestSuiteStorage.Get(SuiteUid).testcases[0].attachments, null);
            Assert.AreEqual(_lifecycle.TestSuiteStorage.Get(SuiteUid).testcases[0].steps, null);
            Assert.AreNotEqual(_lifecycle.TestSuiteStorage.Get(SuiteUid).testcases[0].stop, 0);

            TestCaseStartedEvent evt1 = new TestCaseStartedEvent(SuiteUid, "test name2");
            _lifecycle.Fire(evt1);
            _lifecycle.Fire(new TestCasePendingEvent());
            _lifecycle.Fire(new TestCaseFinishedEvent());

            Assert.IsNull(_lifecycle.TestCaseStorage.Value);
            Assert.AreEqual(_lifecycle.TestSuiteStorage.Get(SuiteUid).testcases.Length, 2);
            Assert.AreEqual(_lifecycle.TestSuiteStorage.Get(SuiteUid).testcases[1].name, "test name2");
            Assert.AreEqual(_lifecycle.TestSuiteStorage.Get(SuiteUid).testcases[1].status, status.pending);
            Assert.AreEqual(_lifecycle.TestSuiteStorage.Get(SuiteUid).testcases[1].failure.message, "Test not implemented yet");
            Assert.AreEqual(_lifecycle.TestSuiteStorage.Get(SuiteUid).testcases[1].attachments, null);
            Assert.AreEqual(_lifecycle.TestSuiteStorage.Get(SuiteUid).testcases[1].steps, null);
            Assert.AreNotEqual(_lifecycle.TestSuiteStorage.Get(SuiteUid).testcases[1].stop, 0);

            TestCaseStartedEvent evt2 = new TestCaseStartedEvent(SuiteUid, "test name3");
            _lifecycle.Fire(evt2);
            _lifecycle.Fire(new TestCaseFailureEvent());
            _lifecycle.Fire(new TestCaseFinishedEvent());

            Assert.IsNull(_lifecycle.TestCaseStorage.Value);
            Assert.AreEqual(_lifecycle.TestSuiteStorage.Get(SuiteUid).testcases.Length, 3);
            Assert.AreEqual(_lifecycle.TestSuiteStorage.Get(SuiteUid).testcases[2].name, "test name3");
            Assert.AreEqual(_lifecycle.TestSuiteStorage.Get(SuiteUid).testcases[2].status, status.broken);
            Assert.AreEqual(_lifecycle.TestSuiteStorage.Get(SuiteUid).testcases[2].failure.message, "Test broken with unknown reason");
            Assert.AreEqual(_lifecycle.TestSuiteStorage.Get(SuiteUid).testcases[2].attachments, null);
            Assert.AreEqual(_lifecycle.TestSuiteStorage.Get(SuiteUid).testcases[2].steps, null);
            Assert.AreNotEqual(_lifecycle.TestSuiteStorage.Get(SuiteUid).testcases[2].stop, 0);
        }
    }
}
