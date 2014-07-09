namespace AllureCSharpCommons
{
    public static class AllureConfig
    {
        /// <summary>
        /// False by default.
        /// If false, empty test suites will not be serialized.
        /// </summary>
        public static bool AllowEmptySuites { get; set; }

        /// <summary>
        /// The directory must be created before running tests.
        /// </summary>
        public static string ResultsPath { get; set; }
    }
}
