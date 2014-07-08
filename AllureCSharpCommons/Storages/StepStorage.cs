using System.Collections.Generic;
using System.Threading;
using AllureCSharpCommons.AllureModel;
using AllureCSharpCommons.Utils;

namespace AllureCSharpCommons.Storages
{
    public class StepStorage
    {
        private readonly ThreadLocal<LinkedList<step>> _threadLocal =
            new ThreadLocal<LinkedList<step>>();

        public step Last
        {
            get { return Get().Last.Value; }
        }

        public LinkedList<step> Get()
        {
            if (_threadLocal.Value == null)
            {
                var queue = new LinkedList<step>();
                queue.AddFirst(CreateRootStep());
                return _threadLocal.Value = queue;
            }
            return _threadLocal.Value;
        }

        public void Put(step step)
        {
            Get().AddLast(step);
        }

        public step PollLast()
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

        public step CreateRootStep()
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

        public void Remove()
        {
            _threadLocal.Value = null;
        }
    }
}