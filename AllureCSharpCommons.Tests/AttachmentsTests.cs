using System.IO;
using AllureCSharpCommons.AllureModel;
using AllureCSharpCommons.Attributes;
using AllureCSharpCommons.Events;
using NUnit.Framework;

namespace AllureCSharpCommons.Tests
{
    [TestFixture]
    public class AttachmentsTests
    {
        private Allure _lifecycle;
        private const string Path = "TestData/attachment";

        [TestFixtureSetUp]
        public void Init()
        {
            Allure.ResultsPath = "AllureResults/";
            Directory.CreateDirectory(Allure.ResultsPath);
            _lifecycle = Allure.Lifecycle;
            _lifecycle.Fire(new TestSuiteStartedEvent("1", "2")
            {
                Labels = new[] {new label {name = "1", value = "1"}}
            });
            _lifecycle.Fire(new TestCaseStartedEvent("1", "2")
            {
                Labels = new[] {new label {name = "1", value = "1"}}
            });
            _lifecycle.Fire(new StepStartedEvent("1"));
        }

        [AllureTitle("asd")]
        [AllureDescription("123", descriptiontype.text)]
        [AllureSeverity(severitylevel.critical)]
        [TestCase("txt", "text/plain")]
        [TestCase("xml", "application/xml")]
        [TestCase("html", "text/html")]
        [TestCase("png", "image/png")]
        [TestCase("jpg", "image/jpeg")]
        [TestCase("json", "application/json")]
        public void AttachmentsTest(string extension, string mime)
        {
            var bytes = File.ReadAllBytes(Path + "." + extension);
            _lifecycle.Fire(new MakeAttachmentEvent(bytes, extension, mime));
        }

        [TestFixtureTearDown]
        public void ShutDown()
        {
            _lifecycle.Fire(new StepFinishedEvent());
            _lifecycle.Fire(new TestCaseFinishedEvent());
            _lifecycle.Fire(new TestSuiteFinishedEvent("1"));
        }
    }
}