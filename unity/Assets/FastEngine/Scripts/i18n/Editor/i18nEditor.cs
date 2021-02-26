using System.Collections;
using System.Collections.Generic;
using FastEngine.Core;
using FastEngine.Core.I18n;
using UnityEditor;
using UnityEngine;


namespace FastEngine.Editor.I18n
{
	public class LocalizationEditor
	{
		/// <summary>
		/// 生成多语言根路径
		/// </summary>
		static string RootPath
		{
			get { return FilePathUtils.Combine(Application.dataPath, "Localization/Language"); }
		}

		/// <summary>
		/// Lua 键值路径
		/// </summary>
		static string LuaKeyPath
		{
			get { return FilePathUtils.Combine(Application.dataPath, "LuaScripts/language.lua"); }
		}

		/// <summary>
		/// CSharp 键值路径
		/// </summary>
		static string CSharpKeyPath
		{
			get { return FilePathUtils.Combine(Application.dataPath, "Scripts/Language.cs"); }
		}

		private static List<SystemLanguage> languages;

		[MenuItem("FastEngine/i18n -> 打开配置", false, 200)]
		static void OpenConfig()
		{
			FastEditorWindow.ShowWindow<EditorWindow>();
		}

		[MenuItem("FastEngine/i18n -> 生成数据", false, 201)]
		public static void Generate()
		{
			var opt = new ExcelReaderOptions();
			opt.languages = Config.ReadEditorDirectory<i18nConfig>().languages;
			var reader = new ExcelReader(opt);
			reader.Read();

			new Excel2Text(reader);
			new Excel2Index(reader);
			new Excel2LuaIndex(reader);

			AssetDatabase.Refresh();

			Debug.Log("i18n generate succeed!");
		}
	}
}