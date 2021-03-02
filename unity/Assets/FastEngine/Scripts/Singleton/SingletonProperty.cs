namespace FastEngine
{
    public class SingletonProperty<T> where T : class, ISingleton
    {
        private static T _instance;

        private static readonly object Obj = new object();

        public static T Instance
        {
            get
            {
                lock (Obj)
                {
                    if (_instance == null)
                        _instance = SingletonCreator.CreateSingleton<T>();
                }
                return _instance;
            }
        }

        public static void Dispose()
        {
            _instance = null;
        }
    }
}