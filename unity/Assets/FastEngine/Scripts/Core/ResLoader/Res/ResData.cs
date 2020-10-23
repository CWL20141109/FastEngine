namespace FastEngine.Core
{
    public class ResData : IPoolObject
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

        public bool isRecycled { get => throw new System.NotImplementedException(); set => throw new System.NotImplementedException(); }

        public void Recycle()
        {
            throw new System.NotImplementedException();
        }
    }
}