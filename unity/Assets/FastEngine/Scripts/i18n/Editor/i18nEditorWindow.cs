using System;
using UnityEngine;
using FastEngine.Core;
using UnityEditor;

namespace FastEngine.Editor.I18n
{
	public class EditorWindow : FastEditorWindow
	{
		private static I18NConfig config;
		private Vector3 _scrollPosition;

		protected override void OnInitialize()
		{
			titleContent.text = "i18n 配置编辑器";
			config = Config.ReadEditorDirectory<I18NConfig>();
		}

		private void OnGUI()
		{
			EditorGUILayout.BeginHorizontal("box");
			if (GUILayout.Button("Save"))
			{
				config.Save<I18NConfig>();
				AssetDatabase.Refresh();
			}

			if (GUILayout.Button("Generate"))
			{
				config.Save<I18NConfig>();
				AssetDatabase.Refresh();
				LocalizationEditor.Generate();
			}

			if (GUILayout.Button(EditorGUIUtility.IconContent("Toolbar Plus")))
			{
				config.languages.Add(SystemLanguage.Unknown);
			}
			EditorGUILayout.EndHorizontal();

			_scrollPosition = EditorGUILayout.BeginScrollView(_scrollPosition);

			for (int i = 0; i < config.languages.Count; i++)
			{
				EditorGUILayout.BeginVertical("box");
				EditorGUILayout.BeginHorizontal();
				config.languages[i] = (SystemLanguage)EditorGUILayout.EnumPopup("", config.languages[i]);
				if (GUILayout.Button(EditorGUIUtility.IconContent("Toolbar Minus")))
				{
					config.languages.RemoveAt(i);
					break;
				}
				EditorGUILayout.EndHorizontal();

				EditorGUILayout.EndVertical();
				EditorGUILayout.Space();
			}

			EditorGUILayout.EndScrollView();
		}
	}
}