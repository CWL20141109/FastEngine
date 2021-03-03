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
        public string BundleName;
        /// <summary>
        /// res 名字
        /// </summary>
        public string AssetName;
    }
    public class AssetBundleDB
    {
        private static bool _initialized;


        private static AssetBundle _manifestBundle;
        private static AssetBundleManifest _manifest;

        static Dictionary<string, AssetBundleMappingData> _mapping = new Dictionary<string, AssetBundleMappingData>();


        /// <summary>
        /// 初始化
        /// </summary>
        public static void Initialize()
        {
            _initialized = true;

            _manifestBundle = AssetBundle.LoadFromFile(FilePathUtils.Combine(AppUtils.BuildRootDirectory(), PlatformUtils.PlatformId()));
            _manifest = _manifestBundle.LoadAsset("AssetBundleManifest") as AssetBundleManifest;

            var ms = File.ReadAllText(FilePathUtils.Combine(AppUtils.BuildRootDirectory(), PlatformUtils.PlatformId() + ".json"));
            _mapping = JsonMapper.ToObject<Dictionary<string, AssetBundleMappingData>>(ms);
        }

        /// <summary>
        /// 通过bundleName 获取依赖列表
        /// </summary>
        /// <param name="bundleName"></param>
        /// <returns></returns>
        public static string[] GetDependencies(string bundleName)
        {
            if (!_initialized) Initialize();
            return _manifest.GetAllDependencies(bundleName);
        }

        /// <summary>
        /// 通过资源路径获取映射数据
        /// </summary>
        /// <param name="resPath"></param>
        /// <returns></returns>
        public static AssetBundleMappingData GetMappingData(string resPath)
        {
            if (!_initialized) Initialize();
            AssetBundleMappingData data = null;
            _mapping.TryGetValue(resPath, out data);
            if (data == null)
            {
                string result = resPath.Replace("Assets/", "");
                result = result.Replace(".png", "");
                result = result.Replace(".prefab", "");
                result = result.Replace(".mp3", "");
                result = result.Replace(".ogg", "");
                result = result.Replace(".wav", "");
                _mapping.TryGetValue(result, out data);
            }
            if (data == null) Debug.LogError("assetbundle mapping data not exist:" + resPath);
            return data;
        }
    }
}

