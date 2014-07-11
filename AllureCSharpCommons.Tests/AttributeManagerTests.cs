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
        public void TestCaseFeaturesAttributeTest()
        {
            var evt = new TestCaseStartedEvent("1", "testcase with title");
            var manager = new AttributeManager(new List<Attribute>
            {
                new AllureFeatureAttribute("Awesome feature")
            });
            manager.Update(evt);
            Assert.AreEqual("feature", evt.Labels[0].name);
            Assert.AreEqual("Awesome feature", evt.Labels[0].value);
        }

        [Test]
        public void TestCaseStroiesAttributeTest()
        {
            var evt = new TestCaseStartedEvent("1", "testcase with title");
            var manager = new AttributeManager(new List<Attribute>
            {
                new AllureStoriesAttribute("Awesome story")
            });
            manager.Update(evt);
            Assert.AreEqual("story", evt.Labels[0].name);
            Assert.AreEqual("Awesome story", evt.Labels[0].value);
        }

        [Test]
        public void TestCaseAttributesTest()
        {
            var evt = new TestCaseStartedEvent("1", "testcase with title");
            var manager = new AttributeManager(new List<Attribute>
            {
                new AllureTitleAttribute("Awesome title"),
                new AllureDescriptionAttribute("Awesome description", descriptiontype.text),
                new AllureSeverityAttribute(severitylevel.critical),
                new AllureFeatureAttribute("Awesome feature", "Another awesome feature", "Awesome feature2", "New awesome feature"),
                new AllureStoriesAttribute("Awesome story", "Another awesome story", "Awesome story2", "New awesome story")
            });
            manager.Update(evt);
            Assert.AreEqual("Awesome title", evt.Title);
            Assert.AreEqual("Awesome description", evt.Description.Value);
            Assert.AreEqual(descriptiontype.text, evt.Description.type);
            Assert.AreEqual("severity", evt.Labels[0].name);
            Assert.AreEqual("critical", evt.Labels[0].value);
            Assert.AreEqual("feature", evt.Labels[1].name);
            Assert.AreEqual("Awesome feature", evt.Labels[1].value);
            Assert.AreEqual("feature", evt.Labels[2].name);
            Assert.AreEqual("Another awesome feature", evt.Labels[2].value);
            Assert.AreEqual("feature", evt.Labels[3].name);
            Assert.AreEqual("Awesome feature2", evt.Labels[3].value);
            Assert.AreEqual("feature", evt.Labels[4].name);
            Assert.AreEqual("New awesome feature", evt.Labels[4].value);
            Assert.AreEqual("story", evt.Labels[5].name);
            Assert.AreEqual("Awesome story", evt.Labels[5].value);
            Assert.AreEqual("story", evt.Labels[6].name);
            Assert.AreEqual("Another awesome story", evt.Labels[6].value);
            Assert.AreEqual("story", evt.Labels[7].name);
            Assert.AreEqual("Awesome story2", evt.Labels[7].value);
            Assert.AreEqual("story", evt.Labels[8].name);
            Assert.AreEqual("New awesome story", evt.Labels[8].value);
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

        [Test]
        public void TestSuiteSeverityAttributeTest()
        {
            var evt = new TestSuiteStartedEvent("1", "testsuite with severity");
            var manager = new AttributeManager(new List<Attribute>
            {
                new AllureSeverityAttribute(severitylevel.critical)
            });
            manager.Update(evt);
            Assert.IsNull(evt.Labels);
        }

        [Test]
        public void TestSuiteFeaturesAttributeTest()
        {
            var evt = new TestSuiteStartedEvent("1", "testsuite with title");
            var manager = new AttributeManager(new List<Attribute>
            {
                new AllureFeatureAttribute("Awesome feature")
            });
            manager.Update(evt);
            Assert.AreEqual("feature", evt.Labels[0].name);
            Assert.AreEqual("Awesome feature", evt.Labels[0].value);
        }

        [Test]
        public void TestSuiteStroiesAttributeTest()
        {
            var evt = new TestSuiteStartedEvent("1", "testsuite with title");
            var manager = new AttributeManager(new List<Attribute>
            {
                new AllureStoriesAttribute("Awesome story")
            });
            manager.Update(evt);
            Assert.AreEqual("story", evt.Labels[0].name);
            Assert.AreEqual("Awesome story", evt.Labels[0].value);
        }

        [Test]
        public void TestSuiteAttributesTest()
        {
            var evt = new TestSuiteStartedEvent("1", "testsuite with title");
            var manager = new AttributeManager(new List<Attribute>
            {
                new AllureTitleAttribute("Awesome title"),
                new AllureDescriptionAttribute("Awesome description", descriptiontype.text),
                new AllureSeverityAttribute(severitylevel.critical),
                new AllureFeatureAttribute("Awesome feature", "Another awesome feature", "Awesome feature2", "New awesome feature"),
                new AllureStoriesAttribute("Awesome story", "Another awesome story", "Awesome story2", "New awesome story")
            });
            manager.Update(evt);
            Assert.AreEqual("Awesome title", evt.Title);
            Assert.AreEqual("Awesome description", evt.Description.Value);
            Assert.AreEqual(descriptiontype.text, evt.Description.type);
            Assert.AreEqual("feature", evt.Labels[0].name);
            Assert.AreEqual("Awesome feature", evt.Labels[0].value);
            Assert.AreEqual("feature", evt.Labels[1].name);
            Assert.AreEqual("Another awesome feature", evt.Labels[1].value);
            Assert.AreEqual("feature", evt.Labels[2].name);
            Assert.AreEqual("Awesome feature2", evt.Labels[2].value);
            Assert.AreEqual("feature", evt.Labels[3].name);
            Assert.AreEqual("New awesome feature", evt.Labels[3].value);
            Assert.AreEqual("story", evt.Labels[4].name);
            Assert.AreEqual("Awesome story", evt.Labels[4].value);
            Assert.AreEqual("story", evt.Labels[5].name);
            Assert.AreEqual("Another awesome story", evt.Labels[5].value);
            Assert.AreEqual("story", evt.Labels[6].name);
            Assert.AreEqual("Awesome story2", evt.Labels[6].value);
            Assert.AreEqual("story", evt.Labels[7].name);
            Assert.AreEqual("New awesome story", evt.Labels[7].value);
        }
    }
}
