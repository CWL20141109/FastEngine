namespace FastEngine.Core
{
    /// <summary>
    ///  标准对象工厂
    /// </summary>
    public class ObjectFactory<T> : IObjectFactory<T> where T : new()
    {
        public T Create()
        {
            return new T();
        }
    }
}