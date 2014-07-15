using System;
using AllureCSharpCommons.AbstractEvents;
using AllureCSharpCommons.AllureModel;
using AllureCSharpCommons.Events;
using AllureCSharpCommons.Storages;
using AllureCSharpCommons.Utils;

namespace AllureCSharpCommons
{
    /// <summary>
    /// Allure C# API.
    /// Used to access Allure lifecycle.
    /// </summary>
    public class Allure
    {
        private static Allure _lifecycle;

        private static readonly Object TestSuiteAddChildLock = new Object();

        protected Allure()
        {
            Logger.Setup();
            StepStorage = new StepStorage();
            TestCaseStorage = new TestCaseStorage();
            TestSuiteStorage = new TestSuiteStorage();
        }

        internal StepStorage StepStorage { get; private set; }
        internal TestCaseStorage TestCaseStorage { get; private set; }
        internal TestSuiteStorage TestSuiteStorage { get; private set; }

        /// <summary>
        /// Allure lifecycle.
        /// Use this to process events.
        /// </summary>
        public static Allure Lifecycle
        {
            get { return _lifecycle ?? DefaultLifecycle; }
            set { _lifecycle = value; }
        }

        /// <summary>
        /// Create new Allure instance.
        /// For tests only.
        /// </summary>
        internal static Allure DefaultLifecycle
        {
            get { return _lifecycle = new Allure(); }
        }

        /// <summary>
        /// Processes all testsuite events.
        /// When processes TestSuiteFinishedEvent serializes it to xml file.
        /// <see cref="AllureCSharpCommons.Events.TestSuiteStartedEvent"/>
        /// <see cref="AllureCSharpCommons.Events.TestSuiteFinishedEvent"/>
        /// </summary>
        /// <param name="evt">event to process</param>
        public void Fire(ITestSuiteEvent evt)
        {
            if (evt.GetType() == typeof (TestSuiteFinishedEvent))
            {
                var suiteUid = evt.Uid;
                var testsuiteresult = TestSuiteStorage.Get(suiteUid);
                
                if (AllureConfig.AllowEmptySuites
                    || (testsuiteresult.testcases != null && testsuiteresult.testcases.Length > 0))
                {
                    evt.Process(testsuiteresult);
                    testsuiteresult.SaveToFile(AllureResultsUtils.TestSuitePath);
                }

                TestSuiteStorage.Remove(suiteUid);
            }
            else
            {
                TestSuiteStorage.Put(evt.Uid);
                var testsuiteresult = TestSuiteStorage.Get(evt.Uid);
                evt.Process(testsuiteresult);
            }
        }

        /// <summary>
        /// Processes all testcase events.
        /// <see cref="AllureCSharpCommons.Events.TestCaseStartedEvent"/>
        /// <see cref="AllureCSharpCommons.Events.TestCasePendingEvent"/>
        /// <see cref="AllureCSharpCommons.Events.TestCaseCanceledEvent"/>
        /// <see cref="AllureCSharpCommons.Events.TestCaseFinishedEvent"/>
        /// </summary>
        /// <param name="evt">event to process</param>
        public void Fire(ITestCaseEvent evt)
        {
            if (evt.GetType() == typeof (TestCaseStartedEvent))
            {
                StepStorage.Get();

                var testcaseresult = TestCaseStorage.Get();
                evt.Process(testcaseresult);

                lock (TestSuiteAddChildLock)
                {
                    TestSuiteStorage.Put(evt.SuiteUid);
                    TestSuiteStorage.Get(evt.SuiteUid).testcases =
                        ArraysUtils.Add(TestSuiteStorage.Get(evt.SuiteUid).testcases, testcaseresult);
                }
            }
            else if (evt.GetType() == typeof (TestCaseFinishedEvent))
            {
                var testcaseresult = TestCaseStorage.Get();
                evt.Process(testcaseresult);

                var root = StepStorage.PollLast();

                testcaseresult.steps = ArraysUtils.AddAll(testcaseresult.steps, root.steps);
                testcaseresult.attachments = ArraysUtils.AddAll(testcaseresult.attachments, root.attachments);
                StepStorage.Remove();
                TestCaseStorage.Remove();
            }
            else
            {
                var testcaseresult = TestCaseStorage.Get();
                evt.Process(testcaseresult);
            }
        }

        /// <summary>
        /// Processes all step events.
        /// <see cref="AllureCSharpCommons.Events.StepStartedEvent"/>
        /// <see cref="AllureCSharpCommons.Events.StepCanceledEvent"/>
        /// <see cref="AllureCSharpCommons.Events.StepFinishedEvent"/>
        /// </summary>
        /// <param name="evt">event to process</param>
        public void Fire(IStepEvent evt)
        {
            if (evt.GetType() == typeof (StepStartedEvent))
            {
                var step = new step();
                evt.Process(step);
                StepStorage.Put(step);
            }
            else if (evt.GetType() == typeof (StepFinishedEvent))
            {
                var step = StepStorage.PollLast();
                evt.Process(step);
                StepStorage.Last.steps = ArraysUtils.Add(StepStorage.Last.steps, step);
            }
            else
            {
                var step = StepStorage.Last;
                evt.Process(step);
            }
        }

        /// <summary>
        /// Clear current step context
        /// </summary>
        /// <param name="evt"></param>
        public void Fire(ClearStepStorageEvent evt)
        {
            StepStorage.Remove();
        }

        /// <summary>
        /// Clear current testcase context
        /// </summary>
        /// <param name="evt"></param>
        public void Fire(ClearTestStorageEvent evt)
        {
            TestCaseStorage.Remove();
        }
    }
}