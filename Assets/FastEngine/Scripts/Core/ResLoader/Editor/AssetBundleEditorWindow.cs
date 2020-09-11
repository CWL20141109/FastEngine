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

            if (GUILayout.Button(EditorGUIUtility.IconContent("Toolbar Plur")))
            {
                if (_bar == 0)
                {
                    var pack = new Pack();
                    pack.editorShow = true;
                    assetBundleConfig.packs.Add(pack);
                }
                else if(_bar == 1)
                {
                    var source = new Source();
                    source.editorShow = true;
                    assetBundleConfig.sources.Add(source);
                }
            }
            EditorGUILayout.EndHorizontal();

            _bar = GUILayout.Toolbar(_bar, _barStrs);

            EditorGUILayout.BeginVertical("box");
        }
    }

}

