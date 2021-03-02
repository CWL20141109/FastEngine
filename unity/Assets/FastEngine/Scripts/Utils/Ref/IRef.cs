namespace FastEngine.Core
{
    public interface IRef
    {
         int RefCount {get;}
         void Retain();
         void Release();
    }
}