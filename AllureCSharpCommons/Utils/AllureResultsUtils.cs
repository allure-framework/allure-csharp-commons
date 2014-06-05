using System;

namespace AllureCSharpCommons.Utils
{
    public static class AllureResultsUtils
    {
        private const string ResultsPath = "";
        
        public static long TimeStamp
        {
            get { return (long) (DateTime.Now - new DateTime(1970, 1, 1)).TotalMilliseconds; }
        }

        public static string GenerateUid()
        {
            return Guid.NewGuid().ToString();
        }

        public static string TestSuitePath
        {
            get { return GenerateUid() + "-testsuite.xml"; }
        }

        public static string AttachmentPath
        {
            get { return ResultsPath + GenerateUid() + "-testsuite.xml"; }
        }
    }
}
