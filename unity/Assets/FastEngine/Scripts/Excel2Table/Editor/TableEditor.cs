/*
* @Author: cwl
* @Description:
* @Date: 2021-02-26 星期五 15:47:01
*/

using FastEngine.Core;
using FastEngine.Core.Excel2Table;
using UnityEditor;
using UnityEngine;
namespace FastEngine.Editor.Excel2Table
{
	public class TableEditor
	{
		[MenuItem("FastEngine/Table -> 打开配置", false, 600)]
		static void OpenConfig()
		{
			FastEditorWindow.ShowWindow<TableEditorWindow>();
		}

		[MenuItem("FastEngine/Table -> 生成数据", false, 601)]
		public static void Generate()
		{
			TableConfig tableConfig = Config.ReadEditorDirectory<TableConfig>();

			FilePathUtils.DirectoryClean(AppUtils.TableDataDirectory());
			FilePathUtils.DirectoryClean(AppUtils.TableObjectDireDirectory());

			tableConfig.TableDictionary.ForEach((item) =>
			{
				Debug.Log("[Excel To Table] Generate : " + item.Value.TableName);
				I18n.LocalizationEditor.Generate();
				var options = new ExcelReaderOptions();
				options.TableName = item.Value.TableName;
				options.TableModelNamespace = tableConfig.TableModelNamespace;
				options.OutFormatOptions = tableConfig.OutFormatOptions;
				options.DataFormatOptions = item.Value.DataFormatOptions;
				options.DataOutDirectory = AppUtils.TableDataDirectory();
				options.TableModelOutDirectory = AppUtils.TableObjectDireDirectory();
				var reader = new ExcelReader($"{AppUtils.TableExcelDirectory()}/{item.Value.TableName}.xlsx", options);
				reader.Read();
				switch (tableConfig.OutFormatOptions)
				{
					case FormatOptions.Csv:
						new Excel2Csv(reader);
						break;
					case FormatOptions.Json:
						new Excel2Json(reader);
						break;
					case FormatOptions.Lua:
						new Excel2Lua(reader);
						break;
				}
				new Excel2TableObject(reader);
			});

			AssetDatabase.Refresh();
			Debug.Log("Table Generate Succeed");
		}
	}
}