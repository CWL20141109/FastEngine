namespace FastEngine.Core
{
    /// <summary>
    /// 池接口
    /// </summary>
    public interface IPool<T>
    {
        int count{get;}

        T Allocate();

        bool Recycle(T obj);
    }
}