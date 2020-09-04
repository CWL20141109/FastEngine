using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FastEngine.Core
{

    public class Config
    {
        static Dictionary<string,ConfigObject> configDict = new Dictionary<string, ConfigObject>();

        public static T ReadDataDirectory<T>() where T : ConfigObject, new()
        {
            
        }

        public static T ReadEditorDirectory<T>() where T : ConfigObject, new()
        {
            string cn = typeof(T).Name;
            ConfigObject co = null;
            if (configDict.TryGetValue(cn, out co))
                return (T) co;

            var cp = FilePathUtils.Combine(AppUtils.)
        }
    }
}

