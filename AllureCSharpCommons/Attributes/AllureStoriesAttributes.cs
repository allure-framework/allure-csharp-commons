using System;

namespace AllureCSharpCommons.Attributes
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
    public class AllureStoriesAttributes : Attribute
    {
        public AllureStoriesAttributes(params string[] features)
        {
            Stories = new string[features.Length];
            Array.Copy(features, Stories, features.Length);
        }

        public string[] Stories { get; private set; }
    }
}