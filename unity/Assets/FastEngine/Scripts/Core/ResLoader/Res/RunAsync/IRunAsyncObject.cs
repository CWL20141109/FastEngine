using System.Collections;
namespace FastEngine.Core
{
    /// <summary>
    /// 异步接口，需要实现此接口才能被 RunAsync 执行
    /// </summary>
    public interface IRunAsyncObject
    {
        IEnumerator AsyncRun(IRunAsync async);
    }
}