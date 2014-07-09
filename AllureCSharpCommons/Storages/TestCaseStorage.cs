using System.Threading;
using AllureCSharpCommons.AllureModel;

namespace AllureCSharpCommons.Storages
{
    internal class TestCaseStorage : ThreadLocal<testcaseresult>
    {
        internal void Put(testcaseresult testcaseresult)
        {
            Value = testcaseresult;
        }

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