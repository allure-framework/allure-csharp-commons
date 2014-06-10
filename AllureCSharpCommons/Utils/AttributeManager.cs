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
                if (x.GetType() == typeof (AllureTitleAttribute))
                {
                    var attr = (AllureTitleAttribute)x;
                    evt.Title = attr.Value;
                }
                else if (x.GetType() == typeof (AllureDescriptionAttribute))
                {
                    var attr = (AllureDescriptionAttribute) x;
                    evt.Description = attr.Value;
                }
            });
        }

        public void Update(TestCaseStartedEvent evt)
        {
            _attributes.ForEach(x =>
            {
                if (x.GetType() == typeof(AllureTitleAttribute))
                {
                    var attr = (AllureTitleAttribute)x;
                    evt.Title = attr.Value;
                }
                else if (x.GetType() == typeof(AllureDescriptionAttribute))
                {
                    var attr = (AllureDescriptionAttribute)x;
                    evt.Description = attr.Value;
                }
                else if (x.GetType() == typeof(AllureSeverityAttribute))
                {
                    var attr = (AllureSeverityAttribute) x;
                    evt.Labels = AllureResultsUtils.Add(evt.Labels, new label()
                    {
                        name = "Severity", 
                        value = attr.Value
                    });
                }
            });
        }
    }
}
