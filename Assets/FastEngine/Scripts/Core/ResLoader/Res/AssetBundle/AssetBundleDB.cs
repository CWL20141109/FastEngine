using System.Collections;
using System.Collections.Generic;
using System.IO;
using LitJson;
using UnityEngine;

namespace FastEngine.Core
{
    /// <summary>
    /// 映射数据
    /// </summary>
    public class AssetBundleMappingData
    {
        /// <summary>
        /// bundle 名字
        /// </summary>
        public string bundleName;
        /// <summary>
        /// res 名字
        /// </summary>
        public string assetName;
    }
    public class AssetBundleDB
    {
        private static bool initialized;


        private static AssetBundle manifestBundle;
        private static AssetBundleManifest manifest;

        static Dictionary<string, AssetBundleMappingData> mapping = new Dictionary<string, AssetBundleMappingData>();


        /// <summary>
        /// 初始化
        /// </summary>
        public static void Initialize()
        {
            initialized = true;

            manifestBundle = AssetBundle.LoadFromFile(FilePathUtils.Combine(AppUtils.BuildRootDirectory(), PlatformUtils.PlatformId()));
            manifest = manifestBundle.LoadAsset("AssetBundleManifest") as AssetBundleManifest;

            var ms = File.ReadAllText(FilePathUtils.Combine(AppUtils.BuildRootDirectory(), PlatformUtils.PlatformId() + ".json"));
            mapping = JsonMapper.ToObject<Dictionary<string, AssetBundleMappingData>>(ms);
        }

        /// <summary>
        /// 通过bundleName 获取依赖列表
        /// </summary>
        /// <param name="bundleName"></param>
        /// <returns></returns>
        public static string[] GetDependencies(string bundleName)
        {
            if (!initialized) Initialize();
            return manifest.GetAllDependencies(bundleName);
        }

        /// <summary>
        /// 通过资源路径获取映射数据
        /// </summary>
        /// <param name="resPath"></param>
        /// <returns></returns>
        public static AssetBundleMappingData GetMappingData(string resPath)
        {
            if (!initialized) Initialize();
            AssetBundleMappingData data = null;
            mapping.TryGetValue(resPath, out data);
            if (data == null)
            {
                string result = resPath.Replace("Assets/", "");
                result = result.Replace(".png", "");
                result = result.Replace(".prefab", "");
                result = result.Replace(".mp3", "");
                result = result.Replace(".ogg", "");
                result = result.Replace(".wav", "");
                mapping.TryGetValue(result, out data);
            }
            if (data == null) Debug.LogError("assetbundle mapping data not exist:" + resPath);
            return data;
        }
    }
}

