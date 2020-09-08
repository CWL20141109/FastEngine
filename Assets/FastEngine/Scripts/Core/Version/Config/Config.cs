using System.Collections;
using System.Collections.Generic;
using LitJson;
using UnityEngine;
using UnityScript.Steps;

namespace FastEngine.Core
{

    public class Config
    {
        static Dictionary<string,ConfigObject> configDict = new Dictionary<string, ConfigObject>();

        public static T ReadDataDirectory<T>() where T : ConfigObject, new()
        {
            return ReadEditorDirectory<T>();
        }

        public static T ReadEditorDirectory<T>() where T : ConfigObject, new()
        {
            string cn = typeof(T).Name;
            ConfigObject co = null;
            if (configDict.TryGetValue(cn, out co))
                return (T) co;

            var cp = FilePathUtils.Combine(AppUtils.ConfigEditorDirectory(), cn + ".json");
            bool succeed = false;
            return Parse<T>(FilePathUtils.FileReadAllText(cp, out succeed));
        }

        /// <summary>
        /// 写入配置
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="data"></param>
        public static void Write<T>(object data)
        {
            var cp = FilePathUtils.Combine(AppUtils.ConfigEditorDirectory(), typeof(T).Name + ".json");
            FilePathUtils.FileWriteAllText(cp, JsonMapper.ToJson(data));
        }

        /// <summary>
        /// 写入配置
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="data">目标目录</param>
        public static void Write<T>(object data,string directory)
        {
            var cp = FilePathUtils.Combine(directory, typeof(T).Name + ".json");
            FilePathUtils.FileWriteAllText(cp, JsonMapper.ToJson(data));
        }

        /// <summary>
        /// 解析对象
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="content"></param>
        /// <returns></returns>
        private static T Parse<T>(string content) where T : ConfigObject, new()
        {
            T obj;
            if (!string.IsNullOrEmpty(content))
                obj = JsonMapper.ToObject<T>(content);
            else obj =new T();
            obj.Initialize();
            return obj;
        }
    }
}

