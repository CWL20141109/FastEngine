using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace FastEngine.Editor.AssetBundle
{

    public class AssetBundleEditor
    {
        [MenuItem("FastEngine/AssetBundle/打开配置")]
        static void OpenConfig()
        {
            FastEditorWindow.ShowWindow<AssetBundleEditorWindow>();
        }
    }
}

