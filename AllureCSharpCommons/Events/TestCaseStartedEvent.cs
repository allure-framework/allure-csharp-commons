using AllureCSharpCommons.AbstractEvents;
using AllureCSharpCommons.AllureModel;
using AllureCSharpCommons.Utils;
using System;

namespace AllureCSharpCommons.Events
{
    public class TestCaseStartedEvent : AbstractTestCaseStartedEvent
    {
        private DateTime? _started;
        
        public TestCaseStartedEvent(string suiteUid, string name, DateTime started)
            : this (suiteUid, name)
        {
            _started = started;
        }
        
        public TestCaseStartedEvent(string suiteUid, string name)
        {
            Name = name;
            SuiteUid = suiteUid;
        }

        public override void Process(testcaseresult context)
        {
            context.start = _started.HasValue ? _started.Value.ToUnixEpochTime() : AllureResultsUtils.TimeStamp;
            context.status = status.passed;
            context.name = Name;
            context.title = Title;
            context.description = Description;
            context.labels = Labels;
        }
    }
}