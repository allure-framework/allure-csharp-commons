using System.Threading;
using AllureCSharpCommons.AllureModel;
using AllureCSharpCommons.Storages;
using NUnit.Framework;

namespace AllureCSharpCommons.Tests
{
    [TestFixture]
    public class StoragesTests
    {
        [Test]
        public void TestCaseStorageTest()
        {
            var storage = new TestCaseStorage();
            var testcaseresult1 = new testcaseresult();
            var testcaseresult2 = new testcaseresult();
            var testcaseresult3 = new testcaseresult();

            storage.Put(testcaseresult1);
            Assert.AreEqual(testcaseresult1, storage.Get());

            new Thread(() =>
            {
                storage.Put(testcaseresult2);
                Thread.Sleep(100);
                Assert.AreEqual(testcaseresult2, storage.Get());
            }).Start();

            new Thread(() =>
            {
                storage.Put(testcaseresult3);
                Assert.AreEqual(testcaseresult3, storage.Get());
            }).Start();
        }

        [Test]
        public void StepStorageTest()
        {
            var storage = new StepStorage();
            var step1 = new step();
            var step2 = new step();
            var step3 = new step();

            storage.Put(step1);
            Assert.AreEqual(2, storage.Get().Count);
            Assert.AreEqual(step1, storage.Last);

            new Thread(() =>
            {
                storage.Put(step2);
                Thread.Sleep(100);
                Assert.AreEqual(2, storage.Get().Count);
                Assert.AreEqual(step2, storage.Last);
            }).Start();

            new Thread(() =>
            {
                storage.Put(step3);
                Assert.AreEqual(2, storage.Get().Count);
                Assert.AreEqual(step3, storage.Last);
            }).Start();
        }

        [Test]
        public void TestSuiteStorageTest()
        {
            var storage = new TestSuiteStorage();
            storage.Put("uid");
            Assert.IsFalse(storage.Put("uid"));

            var testsuiteresult = storage.Get("uid");
            
            Assert.IsNull(testsuiteresult.name);
            Assert.IsNull(testsuiteresult.testcases);
            Assert.AreEqual(0, testsuiteresult.start);
            Assert.AreEqual(0, testsuiteresult.stop);
        }
    }
}
