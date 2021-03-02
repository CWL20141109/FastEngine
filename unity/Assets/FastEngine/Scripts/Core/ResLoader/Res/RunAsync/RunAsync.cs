using System.Collections.Generic;

namespace FastEngine.Core
{
    /// <summary>
    /// 资源异步管理洗头
    /// </summary>
    [MonoSingletonPath("FastEngine/ResLoader/RunAsync")]
    public class RunAsync : MonoSingleton<RunAsync>, IRunAsync
    {
        /// <summary>
        /// 同时运行异步最大数
        /// </summary>
        private const int MAXRunCount = 8;
        /// <summary>
        /// 当前运行异步数量
        /// </summary>
        private int _mRunCount;
        /// <summary>
        /// 异步链表
        /// </summary>
        /// <typeparam name="IRunAsyncObject"></typeparam>
        /// <returns></returns>
        private LinkedList<IRunAsyncObject> _mEnumerators = new LinkedList<IRunAsyncObject>();

        /// <summary>
        ///  添加异步
        /// </summary>
        /// <param name="enumerator"></param>
        public void Push(IRunAsyncObject enumerator)
        {
            _mEnumerators.AddLast(enumerator);
            TryRun();
        }

        /// <summary>
        /// 尝试运行异步
        /// </summary>
        private void TryRun()
        {
            if (_mEnumerators.Count == 0) return;
            if (_mRunCount >= MAXRunCount) return;

            var enumerator = _mEnumerators.First.Value;
            _mEnumerators.RemoveFirst();

            ++_mRunCount;
            StartCoroutine(enumerator.AsyncRun(this));
        }

        /// <summary>
        ///  尝试运行下一个异步
        /// </summary>
        private void TryNextRun()
        {
            --_mRunCount;
            TryRun();
        }

        public void OnRunAsync()
        {
            TryNextRun();
        }
    }
}