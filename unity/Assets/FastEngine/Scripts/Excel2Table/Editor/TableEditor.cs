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

            tableConfig.tableDictionary.ForEach((item) =>
            {
                Debug.Log("[Excel To Table] Generate : " + item.Value.tableName);
                // I18n.LocalizationEditor.Generate();
                var options = new ExcelReaderOptions();
                options.tableName = item.Value.tableName;
                options.tableModelNamespace = tableConfig.tableModelNamespace;
                options.outFormatOptions = tableConfig.outFormatOptions;
                options.dataFormatOptions = item.Value.dataFormatOptions;
                options.dataOutDirectory = AppUtils.TableDataDirectory();
                options.tableModelOutDirectory = AppUtils.TableObjectDireDirectory();
                var reader = new ExcelReader($"{AppUtils.TableExcelDirectory()}/{item.Value.tableName}.xlsx", options);
                reader.Read();
                switch (tableConfig.outFormatOptions)
                {
                    case FormatOptions.CSV:
                        new Excel2Csv(reader);
                        break;
                    case FormatOptions.JSON:
                        new Excel2Json(reader);
                        break;
                    case FormatOptions.LUA:
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