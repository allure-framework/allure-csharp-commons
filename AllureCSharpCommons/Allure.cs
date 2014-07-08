// Author: Ilya Murzinov, https://github.com/ilya-murzinov
// E-mail: murz42@gmail.com
// Project's website: https://github.com/ilya-murzinov/AllureCSharpCommons
// Date: 2014.06.05

using System;
using AllureCSharpCommons.AbstractEvents;
using AllureCSharpCommons.AllureModel;
using AllureCSharpCommons.Events;
using AllureCSharpCommons.Storages;
using AllureCSharpCommons.Utils;
using log4net;

namespace AllureCSharpCommons
{
    public class Allure
    {
        private static Allure _lifecycle;

        private static readonly Object TestSuiteAddChildLock = new Object();

        private static readonly ILog Log = LogManager.GetLogger(typeof (Allure));

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

        public static Allure Lifecycle
        {
            get { return _lifecycle ?? DefaultLifecycle; }
            set { _lifecycle = value; }
        }

        public static Allure DefaultLifecycle
        {
            get { return _lifecycle = new Allure(); }
        }

        public string ResultsPath
        {
            get { return Settings.Default.ResultsPath; }
            set { Settings.Default.ResultsPath = value; }
        }

        public void Fire(ITestSuiteEvent evt)
        {
            if (evt.GetType() == typeof (TestSuiteFinishedEvent))
            {
                string suiteUid = evt.Uid;
                testsuiteresult testsuiteresult = TestSuiteStorage.Get(suiteUid);
                evt.Process(testsuiteresult);
                TestSuiteStorage.Remove(suiteUid);
                testsuiteresult.SaveToFile(AllureResultsUtils.TestSuitePath);
            }
            else
            {
                TestSuiteStorage.Put(evt.Uid);
                testsuiteresult testsuiteresult = TestSuiteStorage.Get(evt.Uid);
                evt.Process(testsuiteresult);
            }
        }

        public void Fire(ITestCaseEvent evt)
        {
            if (evt.GetType() == typeof (TestCaseStartedEvent))
            {
                StepStorage.Get();

                testcaseresult testcaseresult = TestCaseStorage.Get();
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
                testcaseresult testcaseresult = TestCaseStorage.Get();
                evt.Process(testcaseresult);

                step root = StepStorage.PollLast();

                testcaseresult.steps = ArraysUtils.AddRange(testcaseresult.steps, root.steps);
                testcaseresult.attachments = ArraysUtils.AddRange(testcaseresult.attachments, root.attachments);
                StepStorage.Remove();
                TestCaseStorage.Remove();
            }
            else
            {
                testcaseresult testcaseresult = TestCaseStorage.Get();
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
                step step = StepStorage.PollLast();
                evt.Process(step);
                StepStorage.Last.steps = ArraysUtils.Add(StepStorage.Last.steps, step);
            }
            else
            {
                Log.InfoFormat(evt.ToString());
                step step = StepStorage.Last;
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