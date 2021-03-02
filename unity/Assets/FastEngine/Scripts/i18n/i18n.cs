using System.Collections.Generic;
using UnityEngine;

namespace FastEngine.Core
{
	public class I18N
	{
		public static SystemLanguage Language { get; private set; }
		private static Dictionary<int, string[]> _modelDictionary = new Dictionary<int, string[]>();
		private static AssetBundleLoader _resLoader;

		/// <summary>
		/// 初始化
		/// </summary>
		/// <param name="systemLanguage"></param>
		/// <param name="defaultLanguage"> 默认语言 </param>
		public static void Initialize(SystemLanguage systemLanguage, SystemLanguage defaultLanguage)
		{
			Language = systemLanguage;

			if (Language == SystemLanguage.Chinese)
				Language = SystemLanguage.ChineseSimplified;

			var appConfig = Config.ReadResourceDirectory<AppConfig>();
			if (!appConfig.supportedLanuLanguages.Contains(Language))
				Language = defaultLanguage;
		}

		public static string Get(int model, int key)
		{
			string[] ls = null;
			if (!_modelDictionary.TryGetValue(model, out ls))
			{
				BuildModel(model);
			}
			if (_modelDictionary.TryGetValue(model, out ls))
			{
				if (key >= 0 && key < ls.Length)
				{
					return ls[key];
				}
			}
			Debug.LogError("i18n get error! model : " + model + " key : " + key);
			return "";
		}

		static void BuildModel(int model)
		{
			if (_modelDictionary.ContainsKey(model)) return;

			string text = "";
			if (App.Runmodel == AppRunModel.Develop)
			{
				bool succeed = false;
				text = FilePathUtils.FileReadAllText(FilePathUtils.Combine(AppUtils.I18NDataDirectory(), Language.ToString(), Language.ToString(), model + ".txt"), out succeed);
			}
			else
			{
				if (_resLoader == null)
				{
					_resLoader = AssetBundleLoader.Allocate("language/" + Language.ToString().ToLower(), null);
					_resLoader.LoadSync();
				}
				text = _resLoader.Bundleres.AssetBundle.LoadAsset<TextAsset>(model + ".txt").text;
			}
			_modelDictionary.Add(model, text.Split('\n'));
		}
	}
}