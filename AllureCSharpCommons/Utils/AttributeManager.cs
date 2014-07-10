using System;
using System.Collections.Generic;
using System.Linq;
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
                    var attr = (AllureTitleAttribute) x;
                    evt.Title = attr.Value;
                }
                else if (x.GetType() == typeof (AllureDescriptionAttribute))
                {
                    var attr = (AllureDescriptionAttribute) x;
                    evt.Description = attr.Value;
                }
                else if (x.GetType() == typeof(AllureStoriesAttributes))
                {
                    var attr = (AllureStoriesAttributes) x;
                    var labels = attr.Stories.Select(y => new label
                    {
                        name = "story",
                        value = y
                    }).ToArray();
                    evt.Labels = ArraysUtils.AddRange(evt.Labels, labels);
                }
                else if (x.GetType() == typeof(AllureFeatureAttribute))
                {
                    var attr = (AllureFeatureAttribute)x;
                    var labels = attr.Features.Select(y => new label
                    {
                        name = "feature",
                        value = y
                    }).ToArray();
                    evt.Labels = ArraysUtils.AddRange(evt.Labels, labels);
                }
            });
        }

        public void Update(TestCaseStartedEvent evt)
        {
            _attributes.ForEach(x =>
            {
                if (x.GetType() == typeof (AllureTitleAttribute))
                {
                    var attr = (AllureTitleAttribute) x;
                    evt.Title = attr.Value;
                }
                else if (x.GetType() == typeof (AllureDescriptionAttribute))
                {
                    var attr = (AllureDescriptionAttribute) x;
                    evt.Description = attr.Value;
                }
                else if (x.GetType() == typeof(AllureStoriesAttributes))
                {
                    var attr = (AllureStoriesAttributes)x;
                    var labels = attr.Stories.Select(y => new label
                    {
                        name = "story",
                        value = y
                    }).ToArray();
                    evt.Labels = ArraysUtils.AddRange(evt.Labels, labels);
                }
                else if (x.GetType() == typeof(AllureFeatureAttribute))
                {
                    var attr = (AllureFeatureAttribute)x;
                    var labels = attr.Features.Select(y => new label
                    {
                        name = "feature",
                        value = y
                    }).ToArray();
                    evt.Labels = ArraysUtils.AddRange(evt.Labels, labels);
                }
                else if (x.GetType() == typeof (AllureSeverityAttribute))
                {
                    var attr = (AllureSeverityAttribute) x;
                    evt.Labels = ArraysUtils.Add(evt.Labels, new label
                    {
                        name = "severity",
                        value = attr.Value
                    });
                }
            });
        }
    }
}