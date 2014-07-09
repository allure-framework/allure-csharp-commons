using System.Collections.Generic;
using System.Threading;
using AllureCSharpCommons.AllureModel;
using AllureCSharpCommons.Utils;

namespace AllureCSharpCommons.Storages
{
    internal class StepStorage
    {
        private readonly ThreadLocal<LinkedList<step>> _threadLocal =
            new ThreadLocal<LinkedList<step>>();

        internal step Last
        {
            get { return Get().Last.Value; }
        }

        internal LinkedList<step> Get()
        {
            if (_threadLocal.Value == null)
            {
                var queue = new LinkedList<step>();
                queue.AddFirst(CreateRootStep());
                return _threadLocal.Value = queue;
            }
            return _threadLocal.Value;
        }

        internal void Put(step step)
        {
            Get().AddLast(step);
        }

        internal step PollLast()
        {
            var queue = Get();
            var last = queue.Last.Value;
            queue.RemoveLast();
            if (queue.Count == 0)
            {
                queue.AddLast(CreateRootStep());
            }
            return last;
        }

        internal step CreateRootStep()
        {
            var step = new step
            {
                name = "Root step",
                title = "Allure step processing error: if you see this step something went wrong.",
                start = AllureResultsUtils.TimeStamp,
                status = status.broken
            };
            return step;
        }

        internal void Remove()
        {
            _threadLocal.Value = null;
        }
    }
}