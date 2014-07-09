using System.IO;
using AllureCSharpCommons.Events;
using NUnit.Framework;

namespace AllureCSharpCommons.Tests
{
    [TestFixture]
    public class TestSuiteEventTests
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
        public void SuiteFinishedTest()
        {
            _lifecycle = Allure.DefaultLifecycle;
            var tsevt = new TestSuiteStartedEvent(SuiteUid, "suite42");
            _lifecycle.Fire(tsevt);
            var tfevt = new TestSuiteFinishedEvent(SuiteUid);
            _lifecycle.Fire(tfevt);
            Assert.AreEqual(0, _lifecycle.TestSuiteStorage.Map.Count);
            Assert.False(_lifecycle.TestSuiteStorage.Map.ContainsKey(SuiteUid));
        }

        [Test]
        public void SuiteStartedEventTest()
        {
            _lifecycle = Allure.DefaultLifecycle;
            var evt = new TestSuiteStartedEvent(SuiteUid, "suite42");
            _lifecycle.Fire(evt);
            Assert.AreEqual(1, _lifecycle.TestSuiteStorage.Map.Count);
            Assert.True(_lifecycle.TestSuiteStorage.Map.ContainsKey(SuiteUid));
            Assert.AreEqual(null, _lifecycle.TestSuiteStorage.Get(SuiteUid).testcases);
            Assert.AreEqual("suite42", _lifecycle.TestSuiteStorage.Get(SuiteUid).name);
        }
    }
}