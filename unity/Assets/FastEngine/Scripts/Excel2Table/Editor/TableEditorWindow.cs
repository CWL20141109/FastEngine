/*
* @Author: cwl
* @Description:
* @Date: 2021-02-26 星期五 15:49:10
*/

using System.Collections.Generic;
using System.IO;
using FastEngine.Core;
using FastEngine.Core.Excel2Table;
using UnityEngine;
namespace FastEngine.Editor.Excel2Table
{
	public class TableEditorWindow : FastEditorWindow
	{
		private TableConfig m_table;
		private Vector3 m_scrollPosition;

		protected override void OnInitialize()
		{
			titleContent.text = "Table 配置编辑器";
			m_table = Config.ReadEditorDirectory<TableConfig>();

			if (Directory.Exists(AppUtils.TableExcelDirectory()))
			{
				var files = Directory.GetFiles(AppUtils.TableExcelDirectory(), "*.xlsx", SearchOption.AllDirectories);
				HashSet<string> fileHashSet = new HashSet<string>();
				for (int i = 0; i < files.Length; i++)
				{
					var fileName = FilePathUtils.GetFileName(files[i], false);
					fileHashSet.Add(fileName);
					if (!m_table.tableDictionary.ContainsKey(fileName))
					{

					}
				}
			}
		}
	}
}