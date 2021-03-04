namespace FastEngine.Core
{
    /// <summary>
    /// 池对象接口
    /// </summary>
    public interface IPoolObject
    {
        /// <summary>
        /// 回收标识
        /// </summary>
        /// <value></value>
        bool isRecycled { get; set; }
        /// <summary>
        /// 回收
        /// </summary>
        void Recycle();
    }
}

