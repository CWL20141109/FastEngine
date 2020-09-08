using System.Collections;
using System.Collections.Generic;
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

        public 
    }

}

