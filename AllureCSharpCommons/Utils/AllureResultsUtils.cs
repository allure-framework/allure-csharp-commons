using System;

namespace AllureCSharpCommons.Utils
{
    public static class AllureResultsUtils
    {
        private static String _resultsPath = "";

        public static long TimeStamp
        {
            get { return (long) (DateTime.Now - new DateTime(1970, 1, 1)).TotalMilliseconds; }
        }

        public static String GenerateUid()
        {
            return Guid.NewGuid().ToString();
        }

        public static string TestSuitePath
        {
            get { return GenerateUid() + "-testsuite.xml"; }
        }

        public static string AttachmentPath
        {
            get { return _resultsPath + GenerateUid() + "-testsuite.xml"; }
        }
    }
}
