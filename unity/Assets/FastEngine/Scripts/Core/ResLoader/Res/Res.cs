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
        protected string m_assetName;
        /// <summary>
        ///  资源名称
        /// </summary>
        public string assetName { get { return m_assetName; } }

        /// <summary>
        /// bundle名称
        /// </summary>
        protected string m_bundleName;
        /// <summary>
        /// bundle名称
        /// </summary>
        public string bundleName { get { return m_bundleName; } }

        /// <summary>
        /// 资源类型
        /// </summary>
        protected ResState m_state;
        /// <summary>
        /// 资源类型
        /// </summary>
        public ResState state { get { return m_state; } }

        public ResType m_type;
        public ResType type { get { return m_type; } }

        /// <summary>
        /// 资源对象
        /// </summary>
        protected UnityEngine.Object m_asset;
        /// <summary>
        /// 资源对象
        /// </summary>
        public UnityEngine.Object asset { get { return m_asset; } }

        /// <summary>
        /// bundle 对象
        /// </summary>
        protected AssetBundle m_assetBundle;
        /// <summary>
        /// bundle 对象
        /// </summary>
        public AssetBundle assetBundle { get { return m_assetBundle; } }

        /// <summary>
        ///  资源引用计数
        /// </summary>
        private int m_refCount = 0;
        /// <summary>
        ///  资源引用计数
        /// </summary>
        public int refCount { get { return m_refCount; } }

        /// <summary>
        /// 事件监听
        /// </summary>
        protected event ResNotificationListener m_notificationListener;
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
            if (m_asset == null)
                return null;
            return m_asset as T;
        }

        /// <summary>
        /// 添加通知
        /// </summary>
        /// <param name="listener"></param>
        public void AddNotification(ResNotificationListener listener)
        {
            if (listener == null) return;
            m_notificationListener += listener;
        }

        /// <summary>
        ///  移除通知
        /// </summary>
        /// <param name="listener"></param>
        public void RemoveNotification(ResNotificationListener listener)
        {
            if (listener == null) return;
            if (m_notificationListener == null) return;
            m_notificationListener -= listener;
        }

        protected void Notification(bool ready)
        {
            m_notificationListener.InvokeGracefully(ready, this);
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
            ++m_refCount;
        }

        /// <summary>
        /// 减少引用计数
        /// </summary>
        public void Release()
        {
            --m_refCount;
            if (m_refCount <= 0)
            {
                OnZeroRef();
            }
        }

    }
}