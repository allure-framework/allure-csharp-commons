using System;
using System.Collections.Generic;
using AllureCSharpCommons.AllureModel;

namespace AllureCSharpCommons.Storages
{
    public class TestSuiteStorage
    {
        private readonly Dictionary<string, testsuiteresult> _map = 
            new Dictionary<string, testsuiteresult>();

        public testsuiteresult Get(string suiteUid)
        {
            if (!_map.ContainsKey(suiteUid))
            {
                _map.Add(suiteUid, new testsuiteresult());
            }
            return _map[suiteUid];
        }

        public void Remove(string suiteUid)
        {
            _map.Remove(suiteUid);
        }
    }
}
