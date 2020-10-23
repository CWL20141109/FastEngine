using UnityEngine;

namespace FastEngine.Core
{
    public abstract class MonoSingleton<T> : MonoBehaviour, ISingleton where T : MonoSingleton<T>
    {
        protected static T instance = null;
        private static readonly object obj = new object();
        private static bool isQuitApplication = false;

        public static T Instance
        {
            get
            {
                if (isQuitApplication)
                {
                    Debug.LogError(string.Format("Try To Call [MonoSingleton] Instance {0} When The Application Already Quit, return null inside", typeof(T)));
                    return null;
                }
                lock (obj)
                {
                    if (instance == null)
                    {
                        instance = MonoSingletonCreator.CreateMonoSingleton<T>();
                    }
                }
                return MonoSingleton<T>.instance;
            }
        }

        private void Awake()
        {
            isQuitApplication = false;
            this.InitializeSingleton();
        }

        public virtual void Dispose()
        {
            isQuitApplication = true;
            Debug.Log("[MonoSingleton] OnDestroy '" + typeof(T) + "'");
            MonoSingleton<T>.instance = null;
            Destroy(gameObject);
        }

        public virtual void InitializeSingleton() { }

        public static bool HasInstance() { return MonoSingleton<T>.instance != null; }

        protected virtual void OnDestroy()
        {
            if (MonoSingleton<T>.instance != null && MonoSingleton<T>.instance.gameObject == base.gameObject)
                MonoSingleton<T>.instance = (T)((object)null);
        }
    }
}