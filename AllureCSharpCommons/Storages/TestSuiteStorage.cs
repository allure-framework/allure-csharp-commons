using System.Collections.Generic;
using AllureCSharpCommons.AllureModel;

namespace AllureCSharpCommons.Storages
{
    /// <summary>
    /// Stores testsuite results using Dictionary&lt;string, testsuiteresult&gt;.
    /// <see cref="AllureCSharpCommons.AllureModel.testsuiteresult"/>
    /// </summary>
    internal class TestSuiteStorage
    {
        private readonly Dictionary<string, testsuiteresult> _map =
            new Dictionary<string, testsuiteresult>();

        internal Dictionary<string, testsuiteresult> Map
        {
            get { return _map; }
        }

        internal testsuiteresult Get(string suiteUid)
        {
            return _map[suiteUid];
        }

        /// <summary>
        /// Creates new testcaseresult and puts it to the map with provided uid.
        /// <see cref="AllureCSharpCommons.AllureModel.testsuiteresult"/>
        /// </summary>
        /// <param name="uid">suite's unique identifier.</param>
        /// <returns></returns>
        internal bool Put(string uid)
        {
            if (!_map.ContainsKey(uid))
            {
                _map.Add(uid, new testsuiteresult());
                return true;
            }
            return false;
        }

        internal void Remove(string suiteUid)
        {
            _map.Remove(suiteUid);
        }
    }
}