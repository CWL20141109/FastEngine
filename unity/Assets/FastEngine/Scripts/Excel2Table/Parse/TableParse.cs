/*
* @Author: cwl
* @Description: table 解析
* @Date: 2021-03-03 23:39:22
*/


using System.Collections.Generic;
using UnityEngine;
namespace FastEngine.Core.Excel2Table
{
	/// <summary>
	/// table 解析
	/// </summary>
	public abstract class TableParse<T>
	{
		protected string tableName;
		protected FormatOptions format;
		protected string content;

		protected TableParse(string tableName, FormatOptions format)
		{
			this.tableName = tableName;
			this.format = format;
		}

		protected void LoadAsset()
		{
			if (!string.IsNullOrEmpty(content)) return;
			if (App.runModel == AppRunModel.Develop)
			{
				var filePath = FilePathUtils.Combine(AppUtils.TableDataDirectory(), tableName + ".csv");
				bool succeed = false;
				content = FilePathUtils.FileReadAllText(filePath, out succeed);
			}
			else
			{
				var loader = AssetBundleLoader.Allocate(FilePathUtils.Combine(AppUtils.TableDataBundleRootDirectory(), tableName), null);
				loader.LoadSync();
				content = loader.assetRes.GetAsset<TextAsset>().text;
				loader.Unload();
				loader = null;
			}
		}

		/// <summary>
		/// 解析为数组
		/// </summary>
		/// <returns></returns>
		public abstract T[] ParseArray();
		/// <summary>
		/// 解析为字典(key 为 string)
		/// </summary>
		/// <returns></returns>
		public abstract Dictionary<string, T> ParseStringDictionary();
		/// <summary>
		/// 解析为字典(key 为 int)
		/// </summary>
		/// <returns></returns>
		public abstract Dictionary<int, T> ParseIntDictionary();
		/// <summary>
		/// 解析为字典(key 为 int2int)
		/// </summary>
		/// <returns></returns>
		public abstract Dictionary<int, Dictionary<int, T>> ParseInt2IntDictionary();
		
	}
}