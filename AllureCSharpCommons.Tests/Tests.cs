using System.Text;
using AllureCSharpCommons.AllureModel;
using AllureCSharpCommons.Events;
using NUnit.Framework;

namespace AllureCSharpCommons.Tests
{
    [TestFixture]
    public class Tests
    {
        [Test]
        public void MakeAttachmentTest()
        {
            Allure lifecycle = Allure.Lifecycle;
            lifecycle.Fire(new TestSuiteStartedEvent("1", "2")
            {
                Labels = new[] {new label() {name = "1", value = "1"}}
            });
            lifecycle.Fire(new TestCaseStartedEvent("1", "2") {
                Labels = new[] {new label() {name = "1", value = "1"}}
            });
            lifecycle.Fire(new StepStartedEvent("1"));
            lifecycle.Fire(new MakeAttachEvent(Encoding.UTF8.GetBytes("asd"), "asd", "application/xml"));
            lifecycle.Fire(new StepFinishedEvent());
            
            lifecycle.Fire(new TestCaseFinishedEvent());
            lifecycle.Fire(new TestCaseStartedEvent("3", "4")
            {
                Labels = new[] { new label() { name = "12", value = "41" } }
            });
            lifecycle.Fire(new StepStartedEvent("1"));
            lifecycle.Fire(new StepFinishedEvent());
            lifecycle.Fire(new TestCaseFinishedEvent());

            lifecycle.Fire(new TestSuiteFinishedEvent("1"));
            lifecycle.Fire(new TestSuiteFinishedEvent("3"));
        }

        [Test]
        public void Test()
        {
            Allure lifecycle = Allure.Lifecycle;
            lifecycle.Fire(new TestSuiteStartedEvent("1", "2")
            {
                Labels = new[] { new label() { name = "1", value = "1" } }
            });
            lifecycle.Fire(new TestCaseStartedEvent("1", "2")
            {
                Labels = new[] { new label() { name = "1", value = "1" } }
            });

            lifecycle.Fire(new StepStartedEvent("1"));
            lifecycle.Fire(new StepCanceledEvent());
            lifecycle.Fire(new StepFinishedEvent());

            lifecycle.Fire(new TestCaseFinishedEvent());
            lifecycle.Fire(new TestSuiteFinishedEvent("1"));
        }
    }
}