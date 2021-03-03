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
        protected readonly Stack<T> MStacks = new Stack<T>();

        /// <summary>
        /// 对象池数量
        /// </summary>
        /// <value></value>
        public int Count { get { return MStacks.Count; } }
        /// <summary>
        /// 对象工厂
        /// </summary>
        public IObjectFactory<T> MFactory;
        /// <summary>
        /// 池默认最大数量
        /// </summary>
        public int MMAXCount = 12;

        /// <summary>
        /// 分配对象
        /// </summary>
        /// <returns></returns>
        public virtual T Allocate()
        {
            return MStacks.Count == 0 ?MFactory.Create():MStacks.Pop();
        }

        public abstract bool Recycle(T obj);

    }
}