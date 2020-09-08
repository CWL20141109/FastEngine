using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FastEngine
{
    public class AppUtils
    {

        /// <summary>
        /// 根目录
        /// </summary>
        /// <returns></returns>
        public static string FastAssetsRootDirectory()
        {
            return FilePathUtils.Combine(Application.dataPath, "FastAssets");
        }

        #region Table
        /// <summary>
        /// Table excel 配置表目录
        /// </summary>
        /// <returns></returns>
        public static string TableExcelDirectory()
        {
            return FilePathUtils.Combine(FastAssetsRootDirectory(), "Table", "Excel");
        }

        /// <summary>
        /// Table 数据输出
        /// </summary>
        /// <returns></returns>
        public static string TableDataDirectory()
        {
            return FilePathUtils.Combine(FastAssetsRootDirectory(), "Table", "Data");
        }

        #endregion

        #region editor

        public static string ConfigDataDirectory()
        {
            return FilePathUtils.Combine(BuildRootDirectory(), "config");
        }

        /// <summary>
        /// 配置目录-Resource目录
        /// </summary>
        /// <returns></returns>
        public static string ConfigResourceDirectory()
        {
            return "Config";
        }

        /// <summary>
        /// 配置目录-编辑器目录
        /// </summary>
        /// <returns></returns>
        public static string ConfigEditorDirectory()
        {
            return FilePathUtils.Combine(FastAssetsRootDirectory(), "Config");
        }

        /// <summary>
        /// build 根目录
        /// </summary>
        /// <returns></returns>
        public static string BuildRootDirectory()
        {
            return FilePathUtils.Combine(FilePathUtils.GetTopDirectory(Application.dataPath), "Build",
                PlatformUtils.PlatformId());
        }
        #endregion
    }

}

