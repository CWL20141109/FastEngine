using System.Collections.Generic;
using UnityEngine;
namespace FastEngine.Core
{
	public class AppConfig : ConfigObject
	{
		/// <summary>
		/// App 运行模式
		/// </summary>
		public AppRunModel Runmode { get; set; }
		/// <summary>
		/// 开启日志
		/// </summary>
		public bool EnableLog { get; set; }
		/// <summary>
		/// 是否使用系统语言
		/// </summary>
		public bool UseSystemLanguage { get; set; }
		/// <summary>
		/// 指定语言
		/// </summary>
		public SystemLanguage Language { get; set; }
		/// <summary>
		/// 默认语言
		/// </summary>
		public SystemLanguage DefaultLanguage { get; set; }
		/// <summary>
		/// 支持语言
		/// </summary>
		public List<SystemLanguage> supportedLanuLanguages;
		
	}
}