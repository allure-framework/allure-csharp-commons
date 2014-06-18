using System.Configuration;

namespace AllureCSharpCommons.Utils
{
    public class AllureConfig
    {
        public static string Version
        {
            get { return ConfigurationManager.AppSettings["allureVersion"]; }
        }
    }
}
