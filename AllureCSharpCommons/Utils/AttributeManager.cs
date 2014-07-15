using System;
using System.Collections.Generic;
using AllureCSharpCommons.Attributes;
using AllureCSharpCommons.Events;

namespace AllureCSharpCommons.Utils
{
    /// <summary>
    /// Provides methods to update testcase and testsuite results based on their attributes.
    /// <see cref="AllureCSharpCommons.Attributes"/>
    /// </summary>
    public class AttributeManager
    {
        private readonly List<Attribute> _attributes;

        /// <summary>
        /// Create new attribute manager based on list of attributes.
        /// <see cref="AllureCSharpCommons.Attributes"/>
        /// </summary>
        /// <param name="attributes">list of attributes</param>
        public AttributeManager(List<Attribute> attributes)
        {
            _attributes = attributes;
        }

        /// <summary>
        /// Updates TestSuiteStartedEvent based on attributes.
        /// <see cref="AllureCSharpCommons.Attributes"/>
        /// </summary>
        /// <param name="evt"></param>
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
                    var attr = (AllureDescriptionAttribute)x;
                    evt.Description = attr.Value;
                }
                else if (x.GetType() == typeof(AllureStoriesAttribute))
                {
                    var attr = (AllureStoriesAttribute)x;
                    evt.Labels = ArraysUtils.AddLabels(evt.Labels, "story", attr.Stories);
                }
                else if (x.GetType() == typeof(AllureFeaturesAttribute))
                {
                    var attr = (AllureFeaturesAttribute)x;
                    evt.Labels = ArraysUtils.AddLabels(evt.Labels, "feature", attr.Features);
                }
            });
        }

        /// <summary>
        /// Updates TestCaseStartedEvent based on attributes.
        /// <see cref="AllureCSharpCommons.Attributes"/>
        /// </summary>
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
                    var attr = (AllureDescriptionAttribute)x;
                    evt.Description = attr.Value;
                }
                else if (x.GetType() == typeof(AllureStoriesAttribute))
                {
                    var attr = (AllureStoriesAttribute)x;
                    evt.Labels = ArraysUtils.AddLabels(evt.Labels, "story", attr.Stories);
                }
                else if (x.GetType() == typeof(AllureFeaturesAttribute))
                {
                    var attr = (AllureFeaturesAttribute)x;
                    evt.Labels = ArraysUtils.AddLabels(evt.Labels, "feature", attr.Features);
                }
                else if (x.GetType() == typeof (AllureSeverityAttribute))
                {
                    var attr = (AllureSeverityAttribute)x;
                    evt.Labels = ArraysUtils.AddLabel(evt.Labels, "severity", attr.Value);
                }
            });
        }
    }
}