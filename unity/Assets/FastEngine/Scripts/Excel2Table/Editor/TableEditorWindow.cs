/*
* @Author: cwl
* @Description:
* @Date: 2021-02-26 15:49:10
*/

using System.Collections.Generic;
using System.IO;
using FastEngine.Core;
using FastEngine.Core.Excel2Table;
using UnityEditor;
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
                    if (!m_table.TableDictionary.ContainsKey(fileName))
                    {
                        var item = new TableItem();
                        item.TableName = fileName;
                        item.DataFormatOptions = DataFormatOptions.Array;
                        m_table.TableDictionary.Add(fileName, item);
                    }
                }

                List<string> removes = new List<string>();
                m_table.TableDictionary.ForEach((item) =>
                {
                    if (!fileHashSet.Contains(item.Value.TableName))
                    {
                        removes.Add(item.Value.TableName);
                    }
                });

                for (int i = 0; i < removes.Count; i++)
                {
                    m_table.TableDictionary.Remove(removes[i]);
                }
            }
        }

        void OnGUI()
        {
            EditorGUILayout.BeginHorizontal("box");
            if (GUILayout.Button("Save"))
            {
                m_table.Save<TableConfig>();
                AssetDatabase.Refresh();
            }

            if (GUILayout.Button("Generate"))
            {
                m_table.Save<TableConfig>();
                AssetDatabase.Refresh();

            }
        }
    }
}