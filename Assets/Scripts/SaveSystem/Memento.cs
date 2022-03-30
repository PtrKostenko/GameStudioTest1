using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace GameStudioTest1
{
    [System.Serializable]
    public class Memento
    {
        public Dictionary<string, object> KeyValuePairs;
        public string ID;
        public string TypeName;

        public Memento(string id, string typeName)
        {
            ID = id;
            TypeName = typeName;
            KeyValuePairs = new Dictionary<string, object>();
        }

        public void AddKeyValue(string key, object value)
        {
            KeyValuePairs.Add(key, value);
        }

        public object TryGetValue(string key)
        {
            bool success = KeyValuePairs.TryGetValue(key, out object val);
            if (!success)
                Debug.LogError($"No object for such key {key} in memento for {ID}-{TypeName}");
            return val;
        } 
    }
}
