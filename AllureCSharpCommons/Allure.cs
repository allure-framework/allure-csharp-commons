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

        public StepStorage StepStorage { get; private set; }
        public TestCaseStorage TestCaseStorage { get; private set; }
        public TestSuiteStorage TestSuiteStorage { get; private set; }

        protected Allure()
        {
            StepStorage = new StepStorage();
            TestCaseStorage = new TestCaseStorage();
            TestSuiteStorage = new TestSuiteStorage();
        }

        public static Allure Lifecycle
        {
            get { return _lifecycle ?? DefaultLifecycle; }
            set { _lifecycle = value; }
        }

        public static Allure DefaultLifecycle
        {
            get { return _lifecycle = new Allure(); }
        }

        public void Fire(IStepEvent evt)
        {
            if (evt.GetType() == typeof (StepStartedEvent))
            {
                step step = new step();
                evt.Process(step);
                StepStorage.Put(step);
            }
            else if (evt.GetType() == typeof (StepFinishedEvent))
            {
                step step = StepStorage.PollLast();
                evt.Process(step);
                StepStorage.Last.steps.Add(step);
            }
            else
            {
                step step = StepStorage.Last;
                evt.Process(step);
            }
        }

        public void Fire(ITestCaseEvent evt)
        {
            if (evt.GetType() == typeof(TestCaseStartedEvent))
            {
                StepStorage.Get();

                testcaseresult testcaseresult = TestCaseStorage.Get();
                evt.Process(testcaseresult);

                TestSuiteStorage.Get(evt.SuiteUid).testcases.Add(testcaseresult);
            }
            else if (evt.GetType() == typeof(TestCaseFinishedEvent))
            {
                testcaseresult testcaseresult = TestCaseStorage.Get();
                evt.Process(testcaseresult);

                step root = StepStorage.PollLast();

                testcaseresult.steps.AddRange(root.steps);
                testcaseresult.attachments.AddRange(root.attachments);
                StepStorage.Remove();
                TestCaseStorage.Remove();
            }
            else
            {
                testcaseresult testcaseresult = TestCaseStorage.Get();
                evt.Process(testcaseresult);
            }
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
                testsuiteresult testsuiteresult = TestSuiteStorage.Get(evt.Uid);
                evt.Process(testsuiteresult);
            }
        }

        public void Fire(ClearStepStorageEvent evt)
        {
            StepStorage.Dispose();
        }

        public void Fire(ClearTestStorageEvent evt)
        {
            TestCaseStorage.Dispose();
        }    
    }
}
