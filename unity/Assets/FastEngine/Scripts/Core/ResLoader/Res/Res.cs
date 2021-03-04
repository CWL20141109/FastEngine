using UnityEngine;

namespace FastEngine.Core
{
    /// <summary>
    /// 资源通知 Handler
    /// </summary>
    /// <param name="ready"></param>
    /// <param name="res"></param>
    public delegate void ResNotificationListener(bool ready, Res res);

    /// <summary>
    ///  资源基类
    /// </summary>
    public abstract class Res : IRef
    {
        /// <summary>
        ///  资源名称
        /// </summary>
        protected string mAssetName;
        /// <summary>
        ///  资源名称
        /// </summary>
        public string assetName { get { return mAssetName; } }

        /// <summary>
        /// bundle名称
        /// </summary>
        protected string mBundleName;
        /// <summary>
        /// bundle名称
        /// </summary>
        public string bundleName { get { return mBundleName; } }

        /// <summary>
        /// 资源类型
        /// </summary>
        protected ResState mState;
        /// <summary>
        /// 资源类型
        /// </summary>
        public ResState state { get { return mState; } }

        public ResType mType;
        public ResType type { get { return mType; } }

        /// <summary>
        /// 资源对象
        /// </summary>
        protected UnityEngine.Object mAsset;
        /// <summary>
        /// 资源对象
        /// </summary>
        public UnityEngine.Object asset { get { return mAsset; } }

        /// <summary>
        /// bundle 对象
        /// </summary>
        protected AssetBundle mAssetBundle;
        /// <summary>
        /// bundle 对象
        /// </summary>
        public AssetBundle assetBundle { get { return mAssetBundle; } }

        /// <summary>
        ///  资源引用计数
        /// </summary>
        private int _mRefCount = 0;
        /// <summary>
        ///  资源引用计数
        /// </summary>
        public int refCount { get { return _mRefCount; } }

        /// <summary>
        /// 事件监听
        /// </summary>
        protected event ResNotificationListener MNotificationListener;
        /// <summary>
        /// 同步加载
        /// </summary>
        /// <returns></returns>
        public abstract bool LoadSync();
        /// <summary>
        /// 异步加载
        /// </summary>
        /// <returns></returns>
        public abstract bool LoadAsync();
        /// <summary>
        /// 卸载资源
        /// </summary>
        public abstract void Unload();

        /// <summary>
        /// 获得资源
        /// </summary>
        /// <returns>UnityEngine.Object</returns>
        public T GetAsset<T>() where T : UnityEngine.Object
        {
            if (mAsset == null)
                return null;
            return mAsset as T;
        }

        /// <summary>
        /// 添加通知
        /// </summary>
        /// <param name="listener"></param>
        public void AddNotification(ResNotificationListener listener)
        {
            if (listener == null) return;
            MNotificationListener += listener;
        }

        /// <summary>
        ///  移除通知
        /// </summary>
        /// <param name="listener"></param>
        public void RemoveNotification(ResNotificationListener listener)
        {
            if (listener == null) return;
            if (MNotificationListener == null) return;
            MNotificationListener -= listener;
        }

        protected void Notification(bool ready)
        {
            MNotificationListener.InvokeGracefully(ready, this);
        }

        /// <summary>
        /// 引用次数为0的处理
        /// </summary>
        protected abstract void OnZeroRef();

        /// <summary>
        /// 引用计数增加
        /// </summary>
        public void Retain()
        {
            ++_mRefCount;
        }

        /// <summary>
        /// 减少引用计数
        /// </summary>
        public void Release()
        {
            --_mRefCount;
            if (_mRefCount <= 0)
            {
                OnZeroRef();
            }
        }

    }
}