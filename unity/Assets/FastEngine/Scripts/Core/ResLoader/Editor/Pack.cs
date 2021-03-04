using System.Collections;
using System.Collections.Generic;
using System.IO;
using FastEngine.Core;
using FastEngine.Editor.Version;
using UnityEditor;
using UnityEngine;

namespace FastEngine.Editor.AssetBundle
{
    public enum BuildModel
    {
        /// <summary>
        /// 标准形式
        /// </summary>
        Standard,
        /// <summary>
        /// 单一文件打包
        /// </summary>
        File,
        /// <summary>
        /// 整个文件夹打包
        /// </summary>
        Folder,
        /// <summary>
        /// 子文件夹打包
        /// </summary>
        FolderChild,
        /// <summary>
        /// 文件夹内单个文件打包
        /// </summary>
        FolderFile,
    }

    public enum GenerateMapping
    {
        Generate,
        NotGenerate,
    }
    public class Pack
    {
        /// <summary>
        /// 打包方式
        /// </summary>
        public BuildModel model = BuildModel.Standard;
        /// <summary>
        /// 是否生成映射
        /// </summary>
        public GenerateMapping genMapping = GenerateMapping.Generate;
        /// <summary>
        /// 目标路径
        /// </summary>
        public string target;
        /// <summary>
        /// 匹配规则
        /// </summary>
        public string pattern = "*.*";
        /// <summary>
        /// 路径
        /// </summary>
        public string bundlePath;
        /// <summary>
        /// 名字
        /// </summary>
        public string bundleName;
        /// <summary>
        /// 映射配置
        /// </summary>
        public Dictionary<string, AssetBundleMappingData> mapping = new Dictionary<string, AssetBundleMappingData>();

        /// <summary>
        /// 在编辑器中是否显示
        /// </summary>
        public bool editorShow = false;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="structure">结构</param>
        /// <param name="target">目标路径</param>
        /// <param name="bundelPath">bundle 输出路径</param>
        /// <param name="genMapping"> 是否生成mapping配置</param>
        /// <param name="pattern">匹配模式</param>
        /// <returns></returns>
        public static Pack Create(BuildModel structure, string target, string bundelPath, GenerateMapping genMapping,
            string pattern = "*.*")
        {
            var pack = new Pack();
            pack.model = structure;
            pack.target = target;
            pack.bundlePath = bundelPath;
            pack.genMapping = genMapping;
            pack.pattern = pattern;
            return pack;
        }

        public void Build()
        {
            mapping.Clear();

            switch (model)
            {
                case BuildModel.Standard:
                    {
                        BuildeStandard();
                    }
                    break;
                case BuildModel.File:
                    {
                        BuildFile();
                    }
                    break;
                case BuildModel.Folder:
                    {
                        BuildFolder();
                    }
                    break;
                case BuildModel.FolderChild:
                    {
                        BuildFolderChild();
                    }
                    break;
                case BuildModel.FolderFile:
                    {
                        BuildFolderFile();
                    }
                    break;
                default:
                    break;
            }
        }

        private void BuildeStandard()
        {
            string[] typeDirs = Directory.GetDirectories(target, "*", SearchOption.TopDirectoryOnly);
            for (int i = 0; i < typeDirs.Length; i++)
            {
                var typeDir = typeDirs[i];
                var _type = FilePathUtils.GetPathSction(typeDir, -1);
                var dir = Path.Combine(typeDir, "Prefab");
                if (Directory.Exists(dir))
                {
                    string[] files = Directory.GetFiles(dir, pattern, SearchOption.AllDirectories);
                    for (int index = 0; index < files.Length; index++)
                    {
                        var abName = Path.Combine(bundlePath, _type);
                        SetBundleName(files[index], abName);
                    }
                }
            }
        }

        private void BuildFile()
        {
            if (File.Exists(target))
            {
                if (!string.IsNullOrEmpty(bundlePath))
                {
                    SetBundleName(target, FilePathUtils.Combine(bundlePath, FilePathUtils.GetFileName(target, false)));
                }
                else
                {
                    SetBundleName(target, FilePathUtils.GetFileName(target, false));
                }
            }
        }

        private void BuildFolder()
        {
            if (Directory.Exists(target))
            {
                string[] files = Directory.GetFiles(target, pattern, SearchOption.AllDirectories);
                for (int index = 0; index < files.Length; index++)
                {
                    SetBundleName(files[index], FilePathUtils.Combine(bundlePath, bundleName));
                }
            }
        }

        private void BuildFolderChild()
        {
            string[] typeDirs = Directory.GetDirectories(target, "*", SearchOption.TopDirectoryOnly);
            for (int i = 0; i < typeDirs.Length; i++)
            {
                var typeDir = typeDirs[i];
                var _type = FilePathUtils.GetPathSction(typeDir, -1);
                string[] files = Directory.GetFiles(typeDir, pattern, SearchOption.AllDirectories);
                for (int index = 0; index < files.Length; index++)
                {
                    var abName = Path.Combine(bundlePath, _type);
                    SetBundleName(files[index],abName);
                }
            }
        }

        private void BuildFolderFile()
        {
            string[] files = Directory.GetFiles(target, pattern, SearchOption.TopDirectoryOnly);
            for (int i = 0; i < files.Length; i++)
            {
                SetBundleName(files[i], FilePathUtils.Combine(bundlePath, FilePathUtils.GetFileName(files[i], false)));
            }
        }

        private void SetBundleName(string filePath, string bundleName)
        {
            AssetImporter importer = AssetImporter.GetAtPath(filePath);

            if (importer != null)
            {
                importer.assetBundleName = bundleName;

                //添加配置
                var rp = importer.assetPath.Substring("Assets/".Length);
                var pxIndex = rp.LastIndexOf('.');
                if (pxIndex > 0)
                {
                    rp = rp.Substring(0, pxIndex);
                }

                if (!mapping.ContainsKey(rp))
                {
                    var data = new AssetBundleMappingData();
                    data.bundleName = importer.assetBundleName;
                    data.assetName = FilePathUtils.GetFileName(filePath, true);
                    mapping.Add(rp, data);
                }
            }

        }
    }

}

