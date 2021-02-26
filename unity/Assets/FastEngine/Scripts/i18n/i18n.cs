using System.Collections.Generic;
using UnityEngine;

namespace FastEngine.Core
{
	public class i18n
	{
		public static SystemLanguage language { get; private set; }
		private static Dictionary<int, string[]> modelDictionary = new Dictionary<int, string[]>();
		private static AssetBundleLoader resLoader;

		/// <summary>
		/// 初始化
		/// </summary>
		/// <param name="systemLanguage"></param>
		/// <param name="defaultLanguage"> 默认语言 </param>
		public static void Initialize(SystemLanguage systemLanguage, SystemLanguage defaultLanguage)
		{
			language = systemLanguage;

			if (language == SystemLanguage.Chinese)
				language = SystemLanguage.ChineseSimplified;

			var appConfig = Config.ReadResourceDirectory<AppConfig>();
			if (!appConfig.supportedLanuLanguages.Contains(language))
				language = defaultLanguage;
		}

		public static string Get(int model, int key)
		{
			string[] ls = null;
			if (!modelDictionary.TryGetValue(model, out ls))
			{
				BuildModel(model);
			}
			if (modelDictionary.TryGetValue(model, out ls))
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
			if (modelDictionary.ContainsKey(model)) return;

			string text = "";
			if (App.runmodel == AppRunModel.Develop)
			{
				bool succeed = false;
				text = FilePathUtils.FileReadAllText(FilePathUtils.Combine(AppUtils.i18nDataDirectory(), language.ToString(), language.ToString(), model + ".txt"), out succeed);
			}
			else
			{
				if (resLoader == null)
				{
					resLoader = AssetBundleLoader.Allocate("language/" + language.ToString().ToLower(), null);
					resLoader.LoadSync();
				}
				text = resLoader.bundleres.assetBundle.LoadAsset<TextAsset>(model + ".txt").text;
			}
			modelDictionary.Add(model, text.Split('\n'));
		}
	}
}