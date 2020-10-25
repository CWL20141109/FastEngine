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
        private const int maxRunCount = 8;
        /// <summary>
        /// 当前运行异步数量
        /// </summary>
        private int m_runCount;
        /// <summary>
        /// 异步链表
        /// </summary>
        /// <typeparam name="IRunAsyncObject"></typeparam>
        /// <returns></returns>
        private LinkedList<IRunAsyncObject> m_enumerators = new LinkedList<IRunAsyncObject>();

        /// <summary>
        ///  添加异步
        /// </summary>
        /// <param name="enumerator"></param>
        public void Push(IRunAsyncObject enumerator)
        {
            m_enumerators.AddLast(enumerator);
            TryRun();
        }

        /// <summary>
        /// 尝试运行异步
        /// </summary>
        private void TryRun()
        {
            if (m_enumerators.Count == 0) return;
            if (m_runCount >= maxRunCount) return;

            var enumerator = m_enumerators.First.Value;
            m_enumerators.RemoveFirst();

            ++m_runCount;
            StartCoroutine(enumerator.AsyncRun(this));
        }

        /// <summary>
        ///  尝试运行下一个异步
        /// </summary>
        private void TryNextRun()
        {
            --m_runCount;
            TryRun();
        }

        public void OnRunAsync()
        {
            TryNextRun();
        }
    }
}