using UnityEngine;

namespace FastEngine.Core
{
    public class ObjectPool<T> : Pool<T>, ISingleton where T : IPoolObject, new()
    {
        void ISingleton.InitializeSingleton() { }

        protected ObjectPool()
        {
            mFactory = new ObjectFactory<T>();
        }

        public static ObjectPool<T> Instance
        {
            get { return SingletonProperty<ObjectPool<T>>.Instance; }
        }

        public void Dispose()
        {
            SingletonProperty<ObjectPool<T>>.Dispose();
        }

        /// <summary>
        /// 池最大数量
        /// 如果池中数量大于最大数，就移除多余的对象
        /// </summary>
        /// <value></value>
        public int MAXCount
        {
            get { return mMAXCount; }
            set
            {
                mMAXCount = value;
                if (mStacks != null)
                {
                    if (mMAXCount > 0)
                    {
                        if (mMAXCount < mStacks.Count)
                        {
                            int removeCount = mStacks.Count - mMAXCount;
                            while (removeCount > 0)
                            {
                                mStacks.Pop();
                                --removeCount;
                            }
                        }
                    }
                }
            }
        }

        /// <summary>
        /// 初始化
        /// </summary>
        /// <param name="maxCount">池中对象最大数量</param>
        /// <param name="initCount">池对象初始数量</param>
        public void Init(int maxCount, int initCount)
        {
            this.MAXCount = maxCount;

            if (maxCount > 0)
            {
                initCount = Mathf.Min(maxCount, initCount);
            }

            if (Count < initCount)
            {
                for (int i = Count; i < initCount; i++)
                {
                    Recycle(new T());
                }
            }
        }

        /// <summary>
        /// 分配对象
        /// </summary>
        /// <returns></returns>
        public override T Allocate()
        {
            var obj = base.Allocate();
            obj.IsRecycled = false;
            return obj;
        }

        /// <summary>
        /// 回收对象
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public override bool Recycle(T obj)
        {
            if (obj == null || obj.IsRecycled)
                return false;

            if (mMAXCount > 0)
            {
                if (mStacks.Count >= mMAXCount)
                {
                    return false;
                }
            }
            obj.IsRecycled = true;
            mStacks.Push(obj);

            return true;
        }
    }
}