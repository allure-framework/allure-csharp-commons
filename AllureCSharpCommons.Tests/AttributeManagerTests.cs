using System;
using System.Collections.Generic;
using AllureCSharpCommons.AllureModel;
using AllureCSharpCommons.Attributes;
using AllureCSharpCommons.Events;
using AllureCSharpCommons.Utils;
using NUnit.Framework;

namespace AllureCSharpCommons.Tests
{
    [TestFixture]
    public class AttributeManagerTests
    {
        [Test]
        public void TestCaseTitleAttributeTest()
        {
            var evt = new TestCaseStartedEvent("1", "testcase with title");
            var manager = new AttributeManager(new List<Attribute>
            {
                new AllureTitleAttribute("Awesome title")
            });
            manager.Update(evt);
            Assert.AreEqual("Awesome title", evt.Title);
        }

        [Test]
        public void TestCaseDescriptionAttributeTest()
        {
            var evt = new TestCaseStartedEvent("1", "testcase with description");
            var manager = new AttributeManager(new List<Attribute>
            {
                new AllureDescriptionAttribute("Awesome description", descriptiontype.text)
            });
            manager.Update(evt);
            Assert.AreEqual("Awesome description", evt.Description.Value);
            Assert.AreEqual(descriptiontype.text, evt.Description.type);
        }

        [Test]
        public void TestCaseSeverityAttributeTest()
        {
            var evt = new TestCaseStartedEvent("1", "testcase with severity");
            var manager = new AttributeManager(new List<Attribute>
            {
                new AllureSeverityAttribute(severitylevel.critical)
            });
            manager.Update(evt);
            Assert.AreEqual("severity", evt.Labels[0].name);
            Assert.AreEqual("critical", evt.Labels[0].value);
        }

        [Test]
        public void TestSuiteTitleAttributeTest()
        {
            var evt = new TestSuiteStartedEvent("1", "testsuite with title");
            var manager = new AttributeManager(new List<Attribute>
            {
                new AllureTitleAttribute("Awesome title")
            });
            manager.Update(evt);
            Assert.AreEqual("Awesome title", evt.Title);
        }

        [Test]
        public void TestSuiteDescriptionAttributeTest()
        {
            var evt = new TestSuiteStartedEvent("1", "testsuite with description");
            var manager = new AttributeManager(new List<Attribute>
            {
                new AllureDescriptionAttribute("Awesome description", descriptiontype.text)
            });
            manager.Update(evt);
            Assert.AreEqual("Awesome description", evt.Description.Value);
            Assert.AreEqual(descriptiontype.text, evt.Description.type);
        }
    }
}
