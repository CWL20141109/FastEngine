using UnityEngine;

namespace FastEngine
{
    public class MonoSingletonProperty<T> : MonoBehaviour, ISingleton where T : MonoSingletonProperty<T>
    {
        protected static T instance = null;
        private static readonly object Obj = new object();

        public static T Instance
        {
            get
            {
                if (null == instance)
                {
                    instance = MonoSingletonCreator.CreateMonoSingleton<T>();
                }
                return MonoSingletonProperty<T>.instance;
            }
        }

        private void Awake() { this.InitializeSingleton(); }

        public virtual void InitializeSingleton() { }

        public void Dispose()
        {
            GameObject.Destroy(instance.gameObject);
            instance = null;
        }
    }
}