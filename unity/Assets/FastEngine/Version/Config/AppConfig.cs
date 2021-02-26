using System.Collections.Generic;
using UnityEngine;
namespace FastEngine.Core
{
	public class AppConfig : ConfigObject
	{
		/// <summary>
		/// App 运行模式
		/// </summary>
		public AppRunModel runmode { get; set; }
		/// <summary>
		/// 开启日志
		/// </summary>
		public bool enableLog { get; set; }
		/// <summary>
		/// 是否使用系统语言
		/// </summary>
		public bool useSystemLanguage { get; set; }
		/// <summary>
		/// 指定语言
		/// </summary>
		public SystemLanguage language { get; set; }
		/// <summary>
		/// 默认语言
		/// </summary>
		public SystemLanguage defaultLanguage { get; set; }
		/// <summary>
		/// 支持语言
		/// </summary>
		public List<SystemLanguage> supportedLanuLanguages;
		
	}
}