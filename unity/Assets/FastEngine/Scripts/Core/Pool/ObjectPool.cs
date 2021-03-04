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

        public static ObjectPool<T> instance
        {
            get { return SingletonProperty<ObjectPool<T>>.instance; }
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
        public int maxCount
        {
            get { return mmaxCount; }
            set
            {
                mmaxCount = value;
                if (mStacks != null)
                {
                    if (mmaxCount > 0)
                    {
                        if (mmaxCount < mStacks.Count)
                        {
                            int removeCount = mStacks.Count - mmaxCount;
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
            this.maxCount = maxCount;

            if (maxCount > 0)
            {
                initCount = Mathf.Min(maxCount, initCount);
            }

            if (count < initCount)
            {
                for (int i = count; i < initCount; i++)
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
            obj.isRecycled = false;
            return obj;
        }

        /// <summary>
        /// 回收对象
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public override bool Recycle(T obj)
        {
            if (obj == null || obj.isRecycled)
                return false;

            if (mmaxCount > 0)
            {
                if (mStacks.Count >= mmaxCount)
                {
                    return false;
                }
            }
            obj.isRecycled = true;
            mStacks.Push(obj);

            return true;
        }
    }
}