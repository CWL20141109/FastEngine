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

        /// <summary>
        /// 资源类型
        /// </summary>
        private ResType m_type;
        /// <summary>
        /// 资源类型
        /// </summary>
        public ResType type { get { return m_type; } }

        public string poolkey
        {
            get
            {
                switch (m_type)
                {
                    case ResType.Resource:
                        return m_assetName.ToLower();
                    case ResType.Bundle:
                        return m_bundleName.ToLower();
                    case ResType.Asset:
                        return (m_bundleName + m_assetName).ToLower();
                    default:
                        return "";
                }
            }
        }

        public bool isRecycled { get => throw new System.NotImplementedException(); set => throw new System.NotImplementedException(); }

        public void Recycle()
        {
            throw new System.NotImplementedException();
        }
    }
}