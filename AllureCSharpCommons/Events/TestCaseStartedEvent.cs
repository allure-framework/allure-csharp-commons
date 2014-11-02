using AllureCSharpCommons.AbstractEvents;
using AllureCSharpCommons.AllureModel;
using AllureCSharpCommons.Utils;
using System;

namespace AllureCSharpCommons.Events
{
    public class TestCaseStartedEvent : AbstractTestCaseStartedEvent
    {
        private DateTime? mStarted;
        
        public TestCaseStartedEvent(string suiteUid, string name, DateTime started)
            : this (suiteUid, name)
        {
            mStarted = started;
        }
        
        public TestCaseStartedEvent(string suiteUid, string name)
        {
            Name = name;
            SuiteUid = suiteUid;
        }

        public override void Process(testcaseresult context)
        {
            context.start = mStarted.HasValue ? mStarted.Value.ToUnixEpochTime() : AllureResultsUtils.TimeStamp;
            context.status = status.passed;
            context.name = Name;
            context.title = Title;
            context.description = Description;
            context.labels = Labels;
        }
    }
}