using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;


namespace FastEngine.Editor
{

    public abstract class FastEditorWindow : EditorWindow
    {
        public void Initialize()
        {
            OnInitialize();
        }

        protected virtual void OnInitialize() { }

        public static T ShowWindow<T>() where T : FastEditorWindow, new()
        {
            var window = GetWindow<T>(false, "");
            window.Initialize();
            return window;
        }
    }
}

