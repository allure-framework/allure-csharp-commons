using System;

namespace AllureCSharpCommons.Attributes
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
    public class AllureFeaturesAttribute : Attribute
    {
        public AllureFeaturesAttribute(params string[] features)
        {
            Features = new string[features.Length];
            Array.Copy(features, Features, features.Length);
        }

        public string[] Features { get; private set; }
    }
}
