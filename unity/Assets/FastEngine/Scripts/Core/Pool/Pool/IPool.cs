namespace FastEngine.Core
{
    /// <summary>
    /// 池接口
    /// </summary>
    public interface IPool<T>
    {
        int Count{get;}

        T Allocate();

        bool Recycle(T obj);
    }
}