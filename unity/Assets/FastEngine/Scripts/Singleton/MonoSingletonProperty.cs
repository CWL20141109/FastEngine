using UnityEngine;

namespace FastEngine
{
    public class MonoSingletonProperty<T> : MonoBehaviour, ISingleton where T : MonoSingletonProperty<T>
    {
        protected static T _instance = null;
        private static readonly object Obj = new object();

        public static T instance
        {
            get
            {
                if (null == _instance)
                {
                    _instance = MonoSingletonCreator.CreateMonoSingleton<T>();
                }
                return _instance;
            }
        }

        private void Awake() { InitializeSingleton(); }

        public virtual void InitializeSingleton() { }

        public void Dispose()
        {
            GameObject.Destroy(instance.gameObject);
            _instance = null;
        }
    }
}