using System;
using System.Collections.Generic;
using System.Threading;
using AllureCSharpCommons.AllureModel;
using AllureCSharpCommons.Utils;

namespace AllureCSharpCommons.Storages
{
    public class StepStorage : ThreadLocal<LinkedList<step>>
    {
        public LinkedList<step> Get()
        {
            if (Value == null)
            {
                LinkedList<step> queue = new LinkedList<step>();
                queue.AddFirst(CreateRootStep());
                return Value = queue;
            }
            return Value;
        }

        public void Put(step step)
        {
            Get().AddFirst(step);
        }

        public step Last
        {
            get { return Get().Last.Value; }
        }

        public step PollLast()
        {
            LinkedList<step> queue = Get();
            step last = queue.Last.Value;
            queue.RemoveLast();
            if (queue.Count == 0)
            {
                queue.AddFirst(CreateRootStep());
            }
            return last;
        }

        public step CreateRootStep()
        {
            step step = new step
            {
                name = "Root step",
                title = "Allure step processing error: if you see this step something went wrong.",
                start = AllureResultsUtils.TimeStamp,
                status = status.broken
            };
            return step;
        }

        public void Remove()
        {
            Value = null;
        }
    }
}