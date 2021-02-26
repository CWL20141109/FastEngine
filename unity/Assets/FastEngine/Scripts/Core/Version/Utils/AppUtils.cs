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
			return FilePathUtils.Combine(Application.streamingAssetsPath, PlatformUtils.PlatformId());
		}
        #endregion

        #region i18n
		/// <summary>
		/// i18n excel 配置文件路径
		/// </summary>
		/// <returns></returns>
		public static string i18nExcelFilePath()
		{
			return FilePathUtils.Combine(FastAssetsRootDirectory(), "i18n", "Excel", "i18n.xlsx");
		}

		/// <summary>
		/// i18n data 目录
		/// </summary>
		/// <returns></returns>
		public static string i18nDataDirectory()
		{
			return FilePathUtils.Combine(FastAssetsRootDirectory(), "i18n", "Data");
		}

		/// <summary>
		/// i18n index 目录
		/// </summary>
		/// <returns></returns>
		public static string i18nIndexDirectory()
		{
			return FilePathUtils.Combine(FastAssetsRootDirectory(), "i18n", "Index");
		}
		#endregion
	}

}