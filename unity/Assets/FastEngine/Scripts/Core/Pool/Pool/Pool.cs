using System.Collections.Generic;

namespace FastEngine.Core
{
    public abstract class Pool<T> : IPool<T>
    {
        /// <summary>
        /// 池数据
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        protected readonly Stack<T> m_stacks = new Stack<T>();

        /// <summary>
        /// 对象池数量
        /// </summary>
        /// <value></value>
        public int count { get { return m_stacks.Count; } }
        /// <summary>
        /// 对象工厂
        /// </summary>
        public IObjectFactory<T> m_factory;
        /// <summary>
        /// 池默认最大数量
        /// </summary>
        public int m_maxCount = 12;

        /// <summary>
        /// 分配对象
        /// </summary>
        /// <returns></returns>
        public virtual T Allocate()
        {
            return m_stacks.Count == 0 ?m_factory.Create():m_stacks.Pop();
        }

        public abstract bool Recycle(T obj);

    }
}