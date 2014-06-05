using AllureCSharpCommons.AllureModel;
using AllureCSharpCommons.Utils;
using NUnit.Framework;

namespace AllureCSharpCommons.Tests
{
    [TestFixture]
    public class Test
    {
        [Test]
        public void Test1()
        {
            testsuiteresult testcaseresult = new testsuiteresult {name = "42"};
            testcaseresult.SaveToFile(AllureResultsUtils.TestSuitePath);
        }
    }
}
