namespace AllureCSharpCommons
{
    public static class AllureConfig
    {
        private static string _version = "1.4.0";

        /// <summary>
        /// False by default.
        /// If false, empty test suites will not be serialized.
        /// </summary>
        public static bool AllowEmptySuites { get; set; }

        /// <summary>
        /// The directory must be created before running tests.
        /// </summary>
        public static string ResultsPath { get; set; }

        /// <summary>
        /// Version is 1.4.0 by default.
        /// </summary>
        public static string Version
        {
            get { return _version; }
            set { _version = value; }
        }
    }
}
