using System;
using AllureCSharpCommons.AbstractEvents;
using AllureCSharpCommons.AllureModel;
using AllureCSharpCommons.Events;
using AllureCSharpCommons.Storages;
using AllureCSharpCommons.Utils;

namespace AllureCSharpCommons
{
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
            AllowEmptySuites = false;
        }

        internal StepStorage StepStorage { get; private set; }
        internal TestCaseStorage TestCaseStorage { get; private set; }
        internal TestSuiteStorage TestSuiteStorage { get; private set; }

        public static Allure Lifecycle
        {
            get { return _lifecycle ?? DefaultLifecycle; }
            set { _lifecycle = value; }
        }

        public static Allure DefaultLifecycle
        {
            get { return _lifecycle = new Allure(); }
        }

        public static bool AllowEmptySuites { get; set; }

        public static string ResultsPath { get; set; }

        public void Fire(ITestSuiteEvent evt)
        {
            if (evt.GetType() == typeof (TestSuiteFinishedEvent))
            {
                var suiteUid = evt.Uid;
                var testsuiteresult = TestSuiteStorage.Get(suiteUid);
                
                if (AllowEmptySuites
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

                testcaseresult.steps = ArraysUtils.AddRange(testcaseresult.steps, root.steps);
                testcaseresult.attachments = ArraysUtils.AddRange(testcaseresult.attachments, root.attachments);
                StepStorage.Remove();
                TestCaseStorage.Remove();
            }
            else
            {
                var testcaseresult = TestCaseStorage.Get();
                evt.Process(testcaseresult);
            }
        }

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

        public void Fire(ClearStepStorageEvent evt)
        {
            StepStorage.Remove();
        }

        public void Fire(ClearTestStorageEvent evt)
        {
            TestCaseStorage.Remove();
        }
    }
}