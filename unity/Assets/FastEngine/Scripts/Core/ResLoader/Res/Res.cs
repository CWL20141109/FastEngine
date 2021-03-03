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
        protected string MAssetName;
        /// <summary>
        ///  资源名称
        /// </summary>
        public string AssetName { get { return MAssetName; } }

        /// <summary>
        /// bundle名称
        /// </summary>
        protected string MBundleName;
        /// <summary>
        /// bundle名称
        /// </summary>
        public string BundleName { get { return MBundleName; } }

        /// <summary>
        /// 资源类型
        /// </summary>
        protected ResState MState;
        /// <summary>
        /// 资源类型
        /// </summary>
        public ResState State { get { return MState; } }

        public ResType MType;
        public ResType Type { get { return MType; } }

        /// <summary>
        /// 资源对象
        /// </summary>
        protected UnityEngine.Object MAsset;
        /// <summary>
        /// 资源对象
        /// </summary>
        public UnityEngine.Object Asset { get { return MAsset; } }

        /// <summary>
        /// bundle 对象
        /// </summary>
        protected AssetBundle MAssetBundle;
        /// <summary>
        /// bundle 对象
        /// </summary>
        public AssetBundle AssetBundle { get { return MAssetBundle; } }

        /// <summary>
        ///  资源引用计数
        /// </summary>
        private int _mRefCount = 0;
        /// <summary>
        ///  资源引用计数
        /// </summary>
        public int RefCount { get { return _mRefCount; } }

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
            if (MAsset == null)
                return null;
            return MAsset as T;
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