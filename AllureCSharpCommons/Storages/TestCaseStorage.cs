using System.Threading;
using AllureCSharpCommons.AllureModel;

namespace AllureCSharpCommons.Storages
{
    /// <summary>
    /// Stores testcase results using ThreadLocal&lt;LinkedList&lt;step&gt;&gt;.
    /// <see cref="AllureCSharpCommons.AllureModel.testcaseresult"/>
    /// </summary>
    internal class TestCaseStorage : ThreadLocal<testcaseresult>
    {
        internal void Put(testcaseresult testcaseresult)
        {
            Value = testcaseresult;
        }

        /// <summary>
        /// <see cref="AllureCSharpCommons.AllureModel.testcaseresult"/>
        /// </summary>
        /// <returns></returns>
        internal testcaseresult Get()
        {
            if (Value != null)
            {
                return Value;
            }
            return Value = new testcaseresult();
        }

        internal void Remove()
        {
            Value = null;
        }
    }
}