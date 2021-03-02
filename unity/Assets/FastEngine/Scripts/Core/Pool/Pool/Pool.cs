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
        protected readonly Stack<T> mStacks = new Stack<T>();

        /// <summary>
        /// 对象池数量
        /// </summary>
        /// <value></value>
        public int Count { get { return mStacks.Count; } }
        /// <summary>
        /// 对象工厂
        /// </summary>
        public IObjectFactory<T> mFactory;
        /// <summary>
        /// 池默认最大数量
        /// </summary>
        public int mMAXCount = 12;

        /// <summary>
        /// 分配对象
        /// </summary>
        /// <returns></returns>
        public virtual T Allocate()
        {
            return mStacks.Count == 0 ?mFactory.Create():mStacks.Pop();
        }

        public abstract bool Recycle(T obj);

    }
}