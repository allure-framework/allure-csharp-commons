using System;
using AllureCSharpCommons.AllureModel;

namespace AllureCSharpCommons.AllureModel
{
    public partial class parameter
    {
        public parameter() { }

        public parameter(string name, string value, parameterkind kind)
        {
            nameField = name;
            valueField = value;
            kindField = kind;
        }
    }

    public partial class description
    {
        public description(descriptiontype type, string value)
        {
            typeField = type;
            valueField = value;
        }
    }

    public partial class label
    {
        public label() { }

        public label(string name, string value)
        {
            nameField = name;
            valueField = value;
        }
    }

    public partial class attachment
    {
        public attachment() { }

        public attachment(string title, string source, string type, int size)
        { 
            titleField = title;
            sourceField = source;
            typeField = type;
            sizeField = size;
        }
    }

    public partial class step
    {
        public step() { }

        public step(string name, string title, long start, status status)
        {
            nameField = name;
            titleField = title;
            startField = start;
            statusField = status;
        }
    }

    public partial class failure
    {
        public failure() { }

        public failure(string message, string stacktrace)
        {
            messageField = message;

            stacktraceField = stacktrace;
        }
    }
}
