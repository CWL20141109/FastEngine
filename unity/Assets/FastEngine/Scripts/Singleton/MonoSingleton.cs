using UnityEngine;

namespace FastEngine.Core
{
	public abstract class MonoSingleton<T> : MonoBehaviour, ISingleton where T : MonoSingleton<T>
	{
		private static T _instance = null;
		private static readonly object obj = new object();
		private static bool _isQuitApplication = false;

		public static T instance
		{
			get
			{
				if (_isQuitApplication)
				{
					Debug.LogError($"Try To Call [MonoSingleton] Instance {typeof(T)} When The Application Already Quit, return null inside");
					return null;
				}
				lock (obj)
				{
					if (_instance == null)
					{
						_instance = MonoSingletonCreator.CreateMonoSingleton<T>();
					}
				}
				return _instance;
			}
		}

		private void Awake()
		{
			_isQuitApplication = false;
			this.InitializeSingleton();
		}

		public virtual void Dispose()
		{
			_isQuitApplication = true;
			Debug.Log("[MonoSingleton] OnDestroy '" + typeof(T) + "'");
			MonoSingleton<T>._instance = null;
			Destroy(gameObject);
		}

		public virtual void InitializeSingleton() { }

		public static bool HasInstance() { return _instance != null; }

		protected virtual void OnDestroy()
		{
			_instance = null;
		}
	}
}