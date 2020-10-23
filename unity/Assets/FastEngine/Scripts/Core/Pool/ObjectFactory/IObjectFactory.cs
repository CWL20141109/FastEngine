namespace FastEngine.Core
{
    public interface IObjectFactory<T>
    {
        T Create();
    }
}