// Author: Ilya Murzinov, https://github.com/ilya-murzinov
// E-mail: murz42@gmail.com
// Project's website: https://github.com/ilya-murzinov/AllureCSharpCommons
// Date: 2014.06.04

using AllureCSharpCommons.AllureModel;
using AllureCSharpCommons.Events;

namespace AllureCSharpCommons.AbstractEvents
{
    public abstract class AbstractMakeAttachmentEvent : ITestCaseEvent
    {
        public string Title { get; set; }

        public byte[] Attachment { get; set; }

        public string Type { get; set; }

        public abstract void Process(testcaseresult context);
        public string SuiteUid { get; set; }
    }
}