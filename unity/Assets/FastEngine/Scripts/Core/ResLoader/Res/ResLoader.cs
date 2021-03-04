namespace FastEngine.Core
{
    /// <summary>
    /// 加载基类
    /// </summary>
    public abstract class ResLoader
    {
        /// <summary>
        /// 通知
        /// </summary>
        protected ResNotificationListener mListener;

        /// <summary>
        /// 同步加载
        /// </summary>
        /// <returns></returns>
        public abstract bool LoadSync();
        /// <summary>
        ///  异步加载
        /// </summary>
        /// <returns></returns>
        public abstract bool LoadAsync();

        /// <summary>
        /// 卸载资源
        /// </summary>
        public abstract void Unload();

        /// <summary>
        ///接收通知
        /// </summary>
        /// <param name="ready"></param>
        /// <param name="res"></param>
        protected abstract void OnReceiveNotification(bool ready,Res res);
    }
}