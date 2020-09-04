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

        public static stringConfigDataDirectory()
        {
            return FilePathUtils.Combine()
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

        public static string BuildRootDirectory()
        {
            return FilePathUtils.Combine(FilePathUtils.GetTopDirectory(Application.dataPath),"Build",Plac)
        }
        #endregion
    }

}

