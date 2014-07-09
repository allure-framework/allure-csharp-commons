using System;
using System.IO;
using AllureCSharpCommons.Events;
using AllureCSharpCommons.Utils;
using NUnit.Framework;

namespace AllureCSharpCommons.Tests
{
    [TestFixture]
    public class IntegrationTests
    {
        private Allure _lifecycle;
        private const string SuiteUid = "suiteUid";

        [TestFixtureSetUp]
        public void Init()
        {
            Allure.ResultsPath = "AllureResults/";
            Directory.CreateDirectory(Allure.ResultsPath);
        }

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
            _lifecycle.Fire(new MakeAttachmentEvent(File.ReadAllBytes("TestData/attachment.xml"),
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
            _lifecycle.Fire(new MakeAttachmentEvent(File.ReadAllBytes("TestData/attachment.json"),
                "JsonAttachment",
                "application/json"));
            _lifecycle.Fire(new MakeAttachmentEvent(AllureResultsUtils.TakeScreenShot(),
                "Screenshot",
                "image/png"));
            _lifecycle.Fire(new StepFinishedEvent());
            _lifecycle.Fire(new TestCaseFinishedEvent());

            _lifecycle.Fire(new TestCaseStartedEvent(SuiteUid, "failing test case"));
            _lifecycle.Fire(new TestCaseFailureEvent());
            _lifecycle.Fire(new TestCaseFinishedEvent());

            _lifecycle.Fire(new TestCaseStartedEvent(SuiteUid, "failing test case2"));
            _lifecycle.Fire(new TestCaseFailureEvent
            {
                Throwable = new AssertionException("assertion exception"),
                StackTrace = "at com.example.myproject.Book.getTitle(Book.java:16)" +
                             "at com.example.myproject.Author.getBookTitles(Author.java:25)" +
                             "at com.example.myproject.Bootstrap.main(Bootstrap.java:14)"
            });
            _lifecycle.Fire(new TestCaseFinishedEvent());

            _lifecycle.Fire(new TestCaseStartedEvent(SuiteUid, "pending test case"));
            _lifecycle.Fire(new TestCasePendingEvent
            {
                Throwable = new NullReferenceException(),
                StackTrace = "в System.Net.HttpWebRequest.EndGetResponse(IAsyncResult asyncResult)" +
                             "в System.ServiceModel.Channels.HttpChannelFactory`1.HttpRequestChannel.HttpChannelAsyncRequest.CompleteGetResponse(IAsyncResult result)"
            });
            _lifecycle.Fire(new TestCaseFinishedEvent());

            _lifecycle.Fire(new TestCaseStartedEvent(SuiteUid, "pending test case 2"));
            _lifecycle.Fire(new TestCasePendingEvent());
            _lifecycle.Fire(new TestCaseFinishedEvent());

            _lifecycle.Fire(new TestCaseStartedEvent(SuiteUid, "canceled test case"));
            _lifecycle.Fire(new TestCaseCanceledEvent());
            _lifecycle.Fire(new TestCaseFinishedEvent());

            _lifecycle.Fire(new TestSuiteFinishedEvent(SuiteUid));
        }
    }
}