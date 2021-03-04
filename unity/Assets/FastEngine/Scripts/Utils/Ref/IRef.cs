namespace FastEngine.Core
{
    public interface IRef
    {
         int refCount {get;}
         void Retain();
         void Release();
    }
}