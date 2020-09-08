using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace FastEngine.Core
{

    public class ConfigObject
    {
        public void Initialize() { }

        protected virtual void OnInitialize() { }

        /// <summary>
        /// 保存配置
        /// </summary>
        /// <typeparam name="T"></typeparam>
        public void Save<T>() where T :ConfigObject,new ()
        {
           Config.Write<T>(this);
        }

        /// <summary>
        /// 保存配置
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="directory"></param>
        public void Save<T>(string directory) where T : ConfigObject, new()
        {
            Config.Write<T>(this,directory);
        }
    }
}

