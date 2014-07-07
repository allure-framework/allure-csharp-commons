// Author: Ilya Murzinov, https://github.com/ilya-murzinov
// E-mail: murz42@gmail.com
// Project's website: https://github.com/ilya-murzinov/AllureCSharpCommons
// Date: 2014.06.10

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
        public void MultipleTestCasesTest()
        {
            _lifecycle = Allure.DefaultLifecycle;
            var tsevt = new TestSuiteStartedEvent(SuiteUid, "suite42");
            _lifecycle.Fire(tsevt);

            var evt = new TestCaseStartedEvent(SuiteUid, "test name1");
            _lifecycle.Fire(evt);
            _lifecycle.Fire(new TestCaseCanceledEvent());
            _lifecycle.Fire(new TestCaseFinishedEvent());

            Assert.IsNull(_lifecycle.TestCaseStorage.Value);
            Assert.AreEqual(1, _lifecycle.TestSuiteStorage.Get(SuiteUid).testcases.Length);
            Assert.AreEqual("test name1", _lifecycle.TestSuiteStorage.Get(SuiteUid).testcases[0].name);
            Assert.AreEqual(status.canceled, _lifecycle.TestSuiteStorage.Get(SuiteUid).testcases[0].status);
            Assert.AreEqual("Test skipped with unknown reason",
                _lifecycle.TestSuiteStorage.Get(SuiteUid).testcases[0].failure.message);
            Assert.AreEqual(null, _lifecycle.TestSuiteStorage.Get(SuiteUid).testcases[0].attachments);
            Assert.AreEqual(null, _lifecycle.TestSuiteStorage.Get(SuiteUid).testcases[0].steps);
            Assert.AreNotEqual(0, _lifecycle.TestSuiteStorage.Get(SuiteUid).testcases[0].stop);

            var evt1 = new TestCaseStartedEvent(SuiteUid, "test name2");
            _lifecycle.Fire(evt1);
            _lifecycle.Fire(new TestCasePendingEvent());
            _lifecycle.Fire(new TestCaseFinishedEvent());

            Assert.IsNull(_lifecycle.TestCaseStorage.Value);
            Assert.AreEqual(2, _lifecycle.TestSuiteStorage.Get(SuiteUid).testcases.Length);
            Assert.AreEqual("test name2", _lifecycle.TestSuiteStorage.Get(SuiteUid).testcases[1].name);
            Assert.AreEqual(status.pending, _lifecycle.TestSuiteStorage.Get(SuiteUid).testcases[1].status);
            Assert.AreEqual("Test not implemented yet",
                _lifecycle.TestSuiteStorage.Get(SuiteUid).testcases[1].failure.message);
            Assert.AreEqual(null, _lifecycle.TestSuiteStorage.Get(SuiteUid).testcases[1].attachments);
            Assert.AreEqual(null, _lifecycle.TestSuiteStorage.Get(SuiteUid).testcases[1].steps);
            Assert.AreNotEqual(0, _lifecycle.TestSuiteStorage.Get(SuiteUid).testcases[1].stop);

            var evt2 = new TestCaseStartedEvent(SuiteUid, "test name3");
            _lifecycle.Fire(evt2);
            _lifecycle.Fire(new TestCaseFailureEvent());
            _lifecycle.Fire(new TestCaseFinishedEvent());

            Assert.IsNull(_lifecycle.TestCaseStorage.Value);
            Assert.AreEqual(3, _lifecycle.TestSuiteStorage.Get(SuiteUid).testcases.Length);
            Assert.AreEqual("test name3", _lifecycle.TestSuiteStorage.Get(SuiteUid).testcases[2].name);
            Assert.AreEqual(status.broken, _lifecycle.TestSuiteStorage.Get(SuiteUid).testcases[2].status);
            Assert.AreEqual("Test broken with unknown reason",
                _lifecycle.TestSuiteStorage.Get(SuiteUid).testcases[2].failure.message);
            Assert.AreEqual(null, _lifecycle.TestSuiteStorage.Get(SuiteUid).testcases[2].attachments);
            Assert.AreEqual(null, _lifecycle.TestSuiteStorage.Get(SuiteUid).testcases[2].steps);
            Assert.AreNotEqual(0, _lifecycle.TestSuiteStorage.Get(SuiteUid).testcases[2].stop);
        }

        [Test]
        public void TestCaseCanceledEventTest()
        {
            _lifecycle = Allure.DefaultLifecycle;
            var tsevt = new TestSuiteStartedEvent(SuiteUid, "suite42");
            _lifecycle.Fire(tsevt);
            var tcsevt = new TestCaseStartedEvent(SuiteUid, "test name");
            _lifecycle.Fire(tcsevt);
            var evt = new TestCaseCanceledEvent();
            _lifecycle.Fire(evt);
            Assert.AreEqual(status.canceled, _lifecycle.TestSuiteStorage.Get(SuiteUid).testcases[0].status);
            Assert.AreEqual("Test skipped with unknown reason",
                _lifecycle.TestSuiteStorage.Get(SuiteUid).testcases[0].failure.message);
        }

        [Test]
        public void TestCaseFailureWithAssertionExceptionAndStackTraceEventTest()
        {
            _lifecycle = Allure.DefaultLifecycle;
            var tsevt = new TestSuiteStartedEvent(SuiteUid, "suite42");
            _lifecycle.Fire(tsevt);
            var tcsevt = new TestCaseStartedEvent(SuiteUid, "test name");
            _lifecycle.Fire(tcsevt);
            var evt = new TestCaseFailureEvent
            {
                Throwable = new AssertionException("assertion exception"),
                StackTrace = "stack trace"
            };
            _lifecycle.Fire(evt);
            Assert.AreEqual(status.failed, _lifecycle.TestSuiteStorage.Get(SuiteUid).testcases[0].status);
            Assert.AreEqual("assertion exception",
                _lifecycle.TestSuiteStorage.Get(SuiteUid).testcases[0].failure.message);
            Assert.AreEqual("stack trace",
                _lifecycle.TestSuiteStorage.Get(SuiteUid).testcases[0].failure.stacktrace);
        }

        [Test]
        public void TestCaseFailureWithAssertionExceptionWithoutStackTraceEventTest()
        {
            _lifecycle = Allure.DefaultLifecycle;
            var tsevt = new TestSuiteStartedEvent(SuiteUid, "suite42");
            _lifecycle.Fire(tsevt);
            var tcsevt = new TestCaseStartedEvent(SuiteUid, "test name");
            _lifecycle.Fire(tcsevt);
            var evt = new TestCaseFailureEvent
            {
                Throwable = new AssertionException("assertion exception")
            };
            _lifecycle.Fire(evt);
            Assert.AreEqual(status.failed, _lifecycle.TestSuiteStorage.Get(SuiteUid).testcases[0].status);
            Assert.AreEqual("assertion exception",
                _lifecycle.TestSuiteStorage.Get(SuiteUid).testcases[0].failure.message);
            Assert.AreEqual("There is no stack trace",
                _lifecycle.TestSuiteStorage.Get(SuiteUid).testcases[0].failure.stacktrace);
        }

        [Test]
        public void TestCaseFailureWithOtherExceptionEventTest()
        {
            _lifecycle = Allure.DefaultLifecycle;
            var tsevt = new TestSuiteStartedEvent(SuiteUid, "suite42");
            _lifecycle.Fire(tsevt);
            var tcsevt = new TestCaseStartedEvent(SuiteUid, "test name");
            _lifecycle.Fire(tcsevt);
            var evt = new TestCaseFailureEvent
            {
                Throwable = new NullReferenceException("null reference exception")
            };
            _lifecycle.Fire(evt);
            Assert.AreEqual(status.broken, _lifecycle.TestSuiteStorage.Get(SuiteUid).testcases[0].status);
            Assert.AreEqual("null reference exception",
                _lifecycle.TestSuiteStorage.Get(SuiteUid).testcases[0].failure.message);
        }

        [Test]
        public void TestCaseFailureWithoutExceptionEventTest()
        {
            _lifecycle = Allure.DefaultLifecycle;
            var tsevt = new TestSuiteStartedEvent(SuiteUid, "suite42");
            _lifecycle.Fire(tsevt);
            var tcsevt = new TestCaseStartedEvent(SuiteUid, "test name");
            _lifecycle.Fire(tcsevt);
            var evt = new TestCaseFailureEvent();
            _lifecycle.Fire(evt);
            Assert.AreEqual(status.broken, _lifecycle.TestSuiteStorage.Get(SuiteUid).testcases[0].status);
            Assert.AreEqual("Test broken with unknown reason",
                _lifecycle.TestSuiteStorage.Get(SuiteUid).testcases[0].failure.message);
        }

        [Test]
        public void TestCaseFinishedEventAfterTestCaseCancelledEventTest()
        {
            TestCaseCanceledEventTest();
            var evt = new TestCaseFinishedEvent();
            _lifecycle.Fire(evt);
            Assert.IsNull(_lifecycle.TestCaseStorage.Value);
            Assert.AreEqual(1, _lifecycle.TestSuiteStorage.Get(SuiteUid).testcases.Length);
            Assert.AreEqual("test name", _lifecycle.TestSuiteStorage.Get(SuiteUid).testcases[0].name);
            Assert.AreEqual(status.canceled, _lifecycle.TestSuiteStorage.Get(SuiteUid).testcases[0].status);
            Assert.AreEqual("Test skipped with unknown reason",
                _lifecycle.TestSuiteStorage.Get(SuiteUid).testcases[0].failure.message);
            Assert.AreEqual(null, _lifecycle.TestSuiteStorage.Get(SuiteUid).testcases[0].attachments);
            Assert.AreEqual(null, _lifecycle.TestSuiteStorage.Get(SuiteUid).testcases[0].steps);
            Assert.AreNotEqual(0, _lifecycle.TestSuiteStorage.Get(SuiteUid).testcases[0].stop);
        }

        [Test]
        public void TestCaseFinishedEventTest()
        {
            _lifecycle = Allure.DefaultLifecycle;
            var tsevt = new TestSuiteStartedEvent(SuiteUid, "suite42");
            _lifecycle.Fire(tsevt);
            var tcsevt = new TestCaseStartedEvent(SuiteUid, "test name");
            _lifecycle.Fire(tcsevt);
            var evt = new TestCaseFinishedEvent();
            _lifecycle.Fire(evt);
            Assert.IsNull(_lifecycle.TestCaseStorage.Value);
            Assert.AreEqual(1, _lifecycle.TestSuiteStorage.Get(SuiteUid).testcases.Length);
            Assert.AreEqual("test name", _lifecycle.TestSuiteStorage.Get(SuiteUid).testcases[0].name);
            Assert.AreEqual(null, _lifecycle.TestSuiteStorage.Get(SuiteUid).testcases[0].failure);
            Assert.AreEqual(null, _lifecycle.TestSuiteStorage.Get(SuiteUid).testcases[0].attachments);
            Assert.AreEqual(null, _lifecycle.TestSuiteStorage.Get(SuiteUid).testcases[0].steps);
            Assert.AreNotEqual(0, _lifecycle.TestSuiteStorage.Get(SuiteUid).testcases[0].stop);
        }

        [Test]
        public void TestCasePendingEventWithoutMessageWithExceptionTest()
        {
            _lifecycle = Allure.DefaultLifecycle;
            var tsevt = new TestSuiteStartedEvent(SuiteUid, "suite42");
            _lifecycle.Fire(tsevt);
            var tcsevt = new TestCaseStartedEvent(SuiteUid, "test name");
            _lifecycle.Fire(tcsevt);
            var evt = new TestCasePendingEvent
            {
                Throwable = new Exception("exception")
            };
            _lifecycle.Fire(evt);
            Assert.AreEqual(status.pending, _lifecycle.TestSuiteStorage.Get(SuiteUid).testcases[0].status);
            Assert.AreEqual("exception", _lifecycle.TestSuiteStorage.Get(SuiteUid).testcases[0].failure.message);
            Assert.AreEqual("There is no stack trace",
                _lifecycle.TestSuiteStorage.Get(SuiteUid).testcases[0].failure.stacktrace);
        }

        [Test]
        public void TestCasePendingEventWithoutMessageWithoutExceptionTest()
        {
            _lifecycle = Allure.DefaultLifecycle;
            var tsevt = new TestSuiteStartedEvent(SuiteUid, "suite42");
            _lifecycle.Fire(tsevt);
            var tcsevt = new TestCaseStartedEvent(SuiteUid, "test name");
            _lifecycle.Fire(tcsevt);
            var evt = new TestCasePendingEvent();
            _lifecycle.Fire(evt);
            Assert.AreEqual(status.pending, _lifecycle.TestSuiteStorage.Get(SuiteUid).testcases[0].status);
            Assert.AreEqual("Test not implemented yet",
                _lifecycle.TestSuiteStorage.Get(SuiteUid).testcases[0].failure.message);
            Assert.IsNull(_lifecycle.TestSuiteStorage.Get(SuiteUid).testcases[0].failure.stacktrace);
        }

        [Test]
        public void TestCaseStartedEventTest()
        {
            _lifecycle = Allure.DefaultLifecycle;
            var tsevt = new TestSuiteStartedEvent(SuiteUid, "suite42");
            _lifecycle.Fire(tsevt);
            var evt = new TestCaseStartedEvent(SuiteUid, "test name");
            _lifecycle.Fire(evt);
            Assert.AreEqual(true, _lifecycle.TestCaseStorage.IsValueCreated);
            Assert.AreEqual("test name", _lifecycle.TestSuiteStorage.Get(SuiteUid).testcases[0].name);
            Assert.AreEqual(status.passed, _lifecycle.TestSuiteStorage.Get(SuiteUid).testcases[0].status);
            Assert.IsNull(_lifecycle.TestSuiteStorage.Get(SuiteUid).testcases[0].steps);
            Assert.AreNotEqual(0, _lifecycle.TestSuiteStorage.Get(SuiteUid).testcases[0].start);
            Assert.AreEqual(0, _lifecycle.TestSuiteStorage.Get(SuiteUid).testcases[0].stop);
        }
    }
}