using System.Threading;
using AllureCSharpCommons.AllureModel;

namespace AllureCSharpCommons.Storages
{
    public class TestCaseStorage : ThreadLocal<testcaseresult>
    {
        public void Put(testcaseresult testcaseresult)
        {
            Value = testcaseresult;
        }

        public testcaseresult Get()
        {
            if (Value != null)
            {
                return Value;
            }
            return Value = new testcaseresult();
        }

        public void Remove()
        {
            Value = null;
        }
    }
}
