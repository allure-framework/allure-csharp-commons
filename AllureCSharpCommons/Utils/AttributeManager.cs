using System;
using System.Collections.Generic;
using AllureCSharpCommons.AllureModel;
using AllureCSharpCommons.Attributes;
using AllureCSharpCommons.Events;

namespace AllureCSharpCommons.Utils
{
    public class AttributeManager
    {
        private readonly List<Attribute> _attributes;
 
        public AttributeManager(List<Attribute> attributes)
        {
            _attributes = attributes;
        }

        public void Update(TestSuiteStartedEvent evt)
        {
            _attributes.ForEach(x =>
            {
                if (x.GetType() == typeof (AllureTestSuiteAttribute))
                {
                    var attr = (AllureTestSuiteAttribute) x;
                    evt.Title = attr.Title;
                    evt.Description = attr.Description;
                }
            });
        }

        public void Update(TestCaseStartedEvent evt)
        {
            _attributes.ForEach(x =>
            {
                if (x.GetType() == typeof(AllureTestCaseAttribute))
                {
                    var attr = (AllureTestCaseAttribute) x;
                    evt.Title = attr.Title;
                    evt.Description = attr.Description;
                    evt.Labels = AllureResultsUtils.Add(evt.Labels, 
                        new label()
                        {
                            name = "Severity", value = attr.Severity
                        });
                }
            });
        }
    }
}
