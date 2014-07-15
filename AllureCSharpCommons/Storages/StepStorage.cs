using System.Collections.Generic;
using System.Threading;
using AllureCSharpCommons.AllureModel;
using AllureCSharpCommons.Utils;

namespace AllureCSharpCommons.Storages
{
    /// <summary>
    /// Stores steps using ThreadLocal&lt;LinkedList&lt;step&gt;&gt;.
    /// <see cref="AllureCSharpCommons.AllureModel.step"/>
    /// </summary>
    internal class StepStorage
    {
        private readonly ThreadLocal<LinkedList<step>> _threadLocal =
            new ThreadLocal<LinkedList<step>>();

        /// <summary>
        /// Used to inspect problems with Allure lifecycle.
        /// This step is marked broken.
        /// <see cref="AllureCSharpCommons.AllureModel.step"/>
        /// </summary>
        private static readonly step RootStep = new step
        {
            name = "Root step",
            title = "Allure step processing error: if you see this step something went wrong.",
            start = AllureResultsUtils.TimeStamp,
            status = status.broken
        };

        /// <summary>
        /// Retrieves last element of current thread's list without removing it.
        /// <see cref="AllureCSharpCommons.AllureModel.step"/>
        /// </summary>
        internal step Last
        {
            get { return Get().Last.Value; }
        }

        /// <summary>
        /// Creates new LinkedList for current thread if it doesn't already exist.
        /// </summary>
        /// <returns>initial value for current thread</returns>
        internal LinkedList<step> Get()
        {
            if (_threadLocal.Value == null)
            {
                var queue = new LinkedList<step>();
                queue.AddFirst(RootStep);
                return _threadLocal.Value = queue;
            }
            return _threadLocal.Value;
        }
        
        internal void Put(step step)
        {
            Get().AddLast(step);
        }

        /// <summary>
        /// Retrieves and removes last element from current thread's list.
        /// If list is empty after this operation, add root step to it.
        /// <see cref="AllureCSharpCommons.AllureModel.step"/>
        /// </summary>
        /// <returns>last element from current thread's list</returns>
        internal step PollLast()
        {
            var queue = Get();
            var last = queue.Last.Value;
            queue.RemoveLast();
            if (queue.Count == 0)
            {
                queue.AddLast(RootStep);
            }
            return last;
        }

        /// <summary>
        /// Set current thread's list to null.
        /// </summary>
        internal void Remove()
        {
            _threadLocal.Value = null;
        }
    }
}