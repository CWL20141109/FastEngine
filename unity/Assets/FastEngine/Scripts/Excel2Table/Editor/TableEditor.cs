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
                var options = new ExcelReaderOptions();
                options.tableName = item.Value.TableName;
                options.tableModelNamespace = tableConfig.TableModelNamespace;
                options.outFormatOptions = tableConfig.outFormatOptions;
                options.dataFormatOptions = item.Value.DataFormatOptions;
                var reader = new ExcelReader(string.Format("{0}/{1}.xlsx", AppUtils.TableExcelDirectory(), item.Value.TableName), options);
                reader.Read();
                switch (tableConfig.outFormatOptions)
                {
                    case FormatOptions.Csv:
                        break;
                    case FormatOptions.Json:
                        break;
                    case FormatOptions.Lua:
                        break;
                }
            });


        }
    }
}