using System.Collections;
using System.Collections.Generic;
using FastEngine.Core;
using UnityEditor;
using UnityEngine;

namespace FastEngine.Editor.AssetBundle
{
    public class AssetBundleEditorWindow : FastEditorWindow
    {
        private static AssetBundleConfig assetBundleConfig;

        private Vector3 _scrollPosition;
        private int _bar = 0;
        private string[] _barStrs = new[] { "AssetBundle", "Copy Source" };

        protected override void OnInitialize()
        {
            titleContent.text = "AssetBundle 配置编辑器";
            assetBundleConfig = Config.ReadEditorDirectory<AssetBundleConfig>();
        }

        void OnGUI()
        {
            EditorGUILayout.BeginHorizontal("box");
            if (GUILayout.Button("Save"))
            {
                assetBundleConfig.Save<AssetBundleConfig>();
                AssetDatabase.Refresh();
            }

            if (GUILayout.Button(EditorGUIUtility.IconContent("Toolbar Plus")))
            {
                if (_bar == 0)
                {
                    var pack = new Pack();
                    pack.editorShow = true;
                    assetBundleConfig.packs.Add(pack);
                }
                else if (_bar == 1)
                {
                    var source = new Source();
                    source.editorShow = true;
                    assetBundleConfig.sources.Add(source);
                }
            }
            EditorGUILayout.EndHorizontal();

            _bar = GUILayout.Toolbar(_bar, _barStrs);

            EditorGUILayout.BeginVertical("box");
            _scrollPosition = EditorGUILayout.BeginScrollView(_scrollPosition);

            if (_bar == 0)
            {
                for (int i = 0; i < assetBundleConfig.packs.Count; i++)
                {
                    var pack = assetBundleConfig.packs[i];

                    EditorGUILayout.BeginVertical("box");

                    if (pack.editorShow)
                    {
                        EditorGUILayout.BeginHorizontal();
                        EditorGUILayout.LabelField("目标资源路径");
                        if (GUILayout.Button(EditorGUIUtility.IconContent("TreeEditor.Trash")))
                        {
                            assetBundleConfig.packs.RemoveAt(i);
                            break;
                        }

                        if (pack.editorShow && GUILayout.Button(EditorGUIUtility.IconContent("Profiler.NextFrame")))
                        {
                            pack.editorShow = !pack.editorShow;
                        }
                        EditorGUILayout.EndHorizontal();
                        EditorGUILayout.Space();
                        EditorGUILayout.BeginHorizontal();

                        pack.target = EditorGUILayout.TextField(pack.target);

                        if (GUILayout.Button(EditorGUIUtility.IconContent("ViewToolZoom on")))
                        {
                            var tp = EditorUtility.OpenFilePanel("选择目标路径", Application.dataPath, "");
                            if (!string.IsNullOrEmpty(tp))
                            {
                                if (tp.Length > Application.dataPath.Length)
                                {
                                    pack.target = "Assets" + tp.Substring(Application.dataPath.Length,
                                                      tp.Length - Application.dataPath.Length);
                                }
                            }
                        }
                        EditorGUILayout.EndHorizontal();

                        EditorGUILayout.LabelField("AssetBundle 输出路径");
                        pack.bundlePath = EditorGUILayout.TextField(pack.bundlePath);
                        if (pack.model == BuildModel.Folder)
                        {
                            EditorGUILayout.LabelField("AssetBundle 名称");
                            pack.bundleName = EditorGUILayout.TextField(pack.bundleName);
                        }

                        if (pack.model != BuildModel.Standard && pack.model != BuildModel.File)
                        {
                            EditorGUILayout.LabelField("资源匹配模式");
                            pack.pattern = EditorGUILayout.TextField(pack.pattern);
                        }
                        else
                        {
                            pack.pattern = "*.*";
                        }

                        EditorGUILayout.LabelField("导出映射配置");
                        pack.genMapping = (GenerateMapping)EditorGUILayout.EnumFlagsField("", pack.genMapping);
                    }
                    else
                    {
                        EditorGUILayout.BeginHorizontal();
                        EditorGUILayout.LabelField(pack.target);
                        if (GUILayout.Button(EditorGUIUtility.IconContent("TreeEditor.Trash")))
                        {
                            assetBundleConfig.packs.RemoveAt(i);
                            break;
                        }

                        if (!pack.editorShow && GUILayout.Button(EditorGUIUtility.IconContent("editicon.sml")))
                        {
                            pack.editorShow = !pack.editorShow;
                        }
                        EditorGUILayout.EndHorizontal();

                        EditorGUILayout.Space();
                    }

                    EditorGUILayout.EndVertical();
                    EditorGUILayout.Space();
                }

            }
            else if (_bar == 1)
            {
                for (int i = 0; i < assetBundleConfig.sources.Count; i++)
                {
                    var source = assetBundleConfig.sources[i];
                    EditorGUILayout.BeginHorizontal("box");

                    EditorGUILayout.EndScrollView();
                    if (source.editorShow)
                    {
                        EditorGUILayout.BeginHorizontal();
                        EditorGUILayout.LabelField("Source");
                        if (GUILayout.Button(EditorGUIUtility.IconContent("TreeEditor.Trash")))
                        {
                            assetBundleConfig.sources.RemoveAt(i);
                            break;
                        }

                        if (!source.editorShow&&GUILayout.Button(EditorGUIUtility.IconContent("editicon.sml")))
                        {
                            source.editorShow = !source.editorShow;
                        }
                        EditorGUILayout.EndHorizontal();
                    }
                    EditorGUILayout.EndVertical();
                }
            }

            EditorGUILayout.EndScrollView();
            EditorGUILayout.EndVertical();
        }
    }
}

