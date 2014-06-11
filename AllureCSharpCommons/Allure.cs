using System;
using AllureCSharpCommons.AbstractEvents;
using AllureCSharpCommons.AllureModel;
using AllureCSharpCommons.Events;
using AllureCSharpCommons.Storages;
using AllureCSharpCommons.Utils;
using log4net;
using log4net.Config;

namespace AllureCSharpCommons
{
    public class Allure
    {
        private static Allure _lifecycle;

        private static readonly Object TestSuiteAddChildLock = new Object();

        private static readonly ILog Log = LogManager.GetLogger(typeof(Allure));

        protected Allure()
        {
            XmlConfigurator.Configure();
            StepStorage = new StepStorage();
            TestCaseStorage = new TestCaseStorage();
            TestSuiteStorage = new TestSuiteStorage();
            Log.Info("Allure instance instantiated.");
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

        public void Fire(ITestSuiteEvent evt)
        {
            if (evt.GetType() == typeof (TestSuiteFinishedEvent))
            {
                Log.InfoFormat("TestSuiteFinishedEvent: {0}", evt.Uid);
                string suiteUid = evt.Uid;
                testsuiteresult testsuiteresult = TestSuiteStorage.Get(suiteUid);
                evt.Process(testsuiteresult);
                TestSuiteStorage.Remove(suiteUid);
                testsuiteresult.SaveToFile(AllureResultsUtils.TestSuitePath);
            }
            else
            {
                Log.InfoFormat("{0}: {1}", evt.GetType(), evt.Uid);
                TestSuiteStorage.Put(evt.Uid);
                testsuiteresult testsuiteresult = TestSuiteStorage.Get(evt.Uid);
                evt.Process(testsuiteresult);
            }
        }

        public void Fire(ITestCaseEvent evt)
        {
            if (evt.GetType() == typeof (TestCaseStartedEvent))
            {
                Log.InfoFormat("TestCaseStartedEvent: {0}", evt.SuiteUid);
                StepStorage.Get();

                testcaseresult testcaseresult = TestCaseStorage.Get();
                evt.Process(testcaseresult);

                lock (TestSuiteAddChildLock)
                {
                    TestSuiteStorage.Put(evt.SuiteUid);
                    TestSuiteStorage.Get(evt.SuiteUid).testcases =
                        AllureResultsUtils.Add(TestSuiteStorage.Get(evt.SuiteUid).testcases, testcaseresult);
                }
            }
            else if (evt.GetType() == typeof (TestCaseFinishedEvent))
            {
                Log.InfoFormat("TestCaseFinishedEvent: {0}", evt.SuiteUid);
                testcaseresult testcaseresult = TestCaseStorage.Get();
                evt.Process(testcaseresult);

                step root = StepStorage.PollLast();

                testcaseresult.steps = AllureResultsUtils.AddRange(testcaseresult.steps, root.steps);
                testcaseresult.attachments = AllureResultsUtils.AddRange(testcaseresult.attachments, root.attachments);
                StepStorage.Remove();
                TestCaseStorage.Remove();
            }
            else
            {
                Log.InfoFormat("{0}: {1}", evt.GetType(), evt.SuiteUid);
                testcaseresult testcaseresult = TestCaseStorage.Get();
                evt.Process(testcaseresult);
            }
        }

        public void Fire(IStepEvent evt)
        {
            if (evt.GetType() == typeof (StepStartedEvent))
            {
                Log.InfoFormat("StepStartedEvent: {0}", ((StepStartedEvent)evt).Name);
                var step = new step();
                evt.Process(step);
                StepStorage.Put(step);
            }
            else if (evt.GetType() == typeof (StepFinishedEvent))
            {
                Log.InfoFormat("StepFinishedEvent: ");
                step step = StepStorage.PollLast();
                evt.Process(step);
                StepStorage.Last.steps = AllureResultsUtils.Add(StepStorage.Last.steps, step);
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