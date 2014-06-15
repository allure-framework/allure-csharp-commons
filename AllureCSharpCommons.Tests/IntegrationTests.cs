// Author: Ilya Murzinov, https://github.com/ilya-murzinov
// E-mail: murz42@gmail.com
// Project's website: https://github.com/ilya-murzinov/AllureCSharpCommons
// Date: 2014.06.11

using System;
using System.IO;
using AllureCSharpCommons.Events;
using NUnit.Framework;

namespace AllureCSharpCommons.Tests
{
    [TestFixture]
    public class IntegrationTests
    {
        private Allure _lifecycle;
        private const string SuiteUid = "suiteUid";

        [Test]
        public void IntegrationTest()
        {
            _lifecycle = Allure.DefaultLifecycle;
            _lifecycle.Fire(new TestSuiteStartedEvent(SuiteUid, "suite42"));

            _lifecycle.Fire(new TestCaseStartedEvent(SuiteUid, "test case"));
            _lifecycle.Fire(new StepStartedEvent("step1"));
            _lifecycle.Fire(new StepFinishedEvent());
            _lifecycle.Fire(new StepStartedEvent("broken step"));
            _lifecycle.Fire(new StepFailureEvent());
            _lifecycle.Fire(new StepFinishedEvent());
            _lifecycle.Fire(new StepStartedEvent("step with attachment"));
            _lifecycle.Fire(new MakeAttachEvent(File.ReadAllBytes("TestData/attachment.xml"),
                "XmlAttachment",
                "application/xml"));
            _lifecycle.Fire(new StepFinishedEvent());
            _lifecycle.Fire(new TestCaseFinishedEvent());

            _lifecycle.Fire(new TestCaseStartedEvent(SuiteUid, "test case"));
            _lifecycle.Fire(new TestCasePendingEvent());
            _lifecycle.Fire(new StepStartedEvent("step2"));
            _lifecycle.Fire(new StepFinishedEvent());
            _lifecycle.Fire(new StepStartedEvent("failed step"));
            _lifecycle.Fire(new StepFailureEvent
            {
                Throwable = new AssertionException("assertion exception")
            });
            _lifecycle.Fire(new StepFinishedEvent());
            _lifecycle.Fire(new StepStartedEvent("step with attachment"));
            _lifecycle.Fire(new MakeAttachEvent(File.ReadAllBytes("TestData/attachment.json"),
                "XmlAttachment",
                "application/json"));
            _lifecycle.Fire(new StepFinishedEvent());
            _lifecycle.Fire(new TestCaseFinishedEvent());

            _lifecycle.Fire(new TestCaseStartedEvent(SuiteUid, "failing test case"));
            _lifecycle.Fire(new TestCaseFailureEvent());
            _lifecycle.Fire(new TestCaseFinishedEvent());

            _lifecycle.Fire(new TestCaseStartedEvent(SuiteUid, "failing test case2"));
            _lifecycle.Fire(new TestCaseFailureEvent
            {
                Throwable = new AssertionException("assertion exception")
            });
            _lifecycle.Fire(new TestCaseFinishedEvent());

            _lifecycle.Fire(new TestCaseStartedEvent(SuiteUid, "pending test case"));
            _lifecycle.Fire(new TestCasePendingEvent
            {
                Throwable = new NullReferenceException()
            });

            _lifecycle.Fire(new TestCaseStartedEvent(SuiteUid, "pending test case"));
            _lifecycle.Fire(new TestCasePendingEvent());
            _lifecycle.Fire(new TestCaseFinishedEvent());

            _lifecycle.Fire(new TestCaseStartedEvent(SuiteUid, "canceled test case"));
            _lifecycle.Fire(new TestCaseCanceledEvent());
            _lifecycle.Fire(new TestCaseFinishedEvent());

            _lifecycle.Fire(new TestSuiteFinishedEvent(SuiteUid));
        }
    }
}