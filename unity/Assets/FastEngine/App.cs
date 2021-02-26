using FastEngine.Core;
namespace FastEngine
{
	/// <summary>
	/// 运行模式
	/// </summary>
	/// 开发模式
	///		- 热更新: 关闭
	///		- Localization:非Bundle加载
	///		- UI:非Bundle加载
	///		- 其他资源:Bundle加载
	/// 正式模式
	///		- 热更新: 开启
	///		- Localization:Bundle加载
	///		- UI:Bundle加载
	///		- 其他资源:Bundle加载	
	public enum AppRunModel
	{
		/// <summary>
		/// 开发模式
		/// </summary>
		/// -关闭热更新
		Develop,
		/// <summary>
		/// 正式模式
		/// </summary>
		/// -开启热更新
		/// -开启bundle资源加载
		/// -开启日记
		Release,
		/// <summary>
		/// 测试模式
		/// </summary>
		/// - 开启热更新
		/// - 开启bundle资源加载
		/// - 开启日志
		Test,
		/// <summary>
		/// 测试线上pc包
		/// </summary>
		/// - 关闭热更新
		/// - 开启bundle资源加载
		/// - 关闭日志
		TestRelease,
	}
	public class App : MonoSingleton<App>
	{
		/// <summary>
		/// 运行模式
		/// </summary>
		public static AppRunModel runmodel { get; private set; }
		
	}
}