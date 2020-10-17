using System.Collections;
using System.Collections.Generic;
using System.IO;
using FastEngine.Core;
using LitJson;
using UnityEditor;
using UnityEngine;

namespace FastEngine.Editor.AssetBundle
{

    public class AssetBundleEditor
    {
        [MenuItem("FastEngine/AssetBundle -> 打开配置",false,100)]
        static void OpenConfig()
        {
            FastEditorWindow.ShowWindow<AssetBundleEditorWindow>();
        }

        [MenuItem("FastEngine/AssetBundle -> 打包", false, 101)]
        static void Build()
        {

        }

        [MenuItem("FastEngine/AssetBundle -> 生成映射配置",false,103)]
        static void GenMapping()
        {
            StartGenMapping();
        }

        [MenuItem("FastEngine/AssetBundle -> Copy Source")]
        static void CopySource()
        {
            AssetBundleConfig config = Config.ReadEditorDirectory<AssetBundleConfig>();
            for (int i = 0; i < config.sources.Count; i++)
            {
                var source = config.sources[i];
                if (File.Exists(source.source)) FilePathUtils.FileCopy(source.source, source.dest);
                else FilePathUtils.Dir;
            }
        }

        static bool StartGenMapping()
        {
            AssetDatabase.RemoveUnusedAssetBundleNames();
            AssetBundleConfig config = Config.ReadEditorDirectory<AssetBundleConfig>();
            Dictionary<string,AssetBundleMappingData> mapingDic= new Dictionary<string, AssetBundleMappingData>();
            for (int i = 0; i < config.packs.Count; i++)
            {
                var pack = config.packs[i];
                pack.Build();
                if (pack.genMapping == GenerateMapping.Generate)
                {
                    foreach (var dataItem in pack.mapping)
                    {
                        mapingDic.Add(dataItem.Key,dataItem.Value);
                    }
                }
            }

            //映射配置写入文件
            var mp = FilePathUtils.Combine(AppUtils.BuildRootDirectory(), PlatformUtils.PlatformId() + ".json");
            if (File.Exists(mp))
            {
                File.Delete(mp);
            }

            if (!Directory.Exists(AppUtils.BuildRootDirectory()))
            {
                Directory.CreateDirectory(AppUtils.BuildRootDirectory());
            }
            File.WriteAllText(mp,JsonMapper.ToJson((mapingDic)));
            return true;
        }
    }
}

