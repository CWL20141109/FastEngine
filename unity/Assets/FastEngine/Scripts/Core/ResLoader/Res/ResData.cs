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
        
        /// <summary>
        /// bundleRes
        /// </summary>
        /// <param name="bundleName"></param>
        /// <returns></returns>
        public static ResData AlloocateBundle(string bundleName)
        {
            return Allocate("", bundleName, ResType.Bundle);
        }
        
        /// <summary>
        /// 分配asset 对象
        /// </summary>
        /// <param name="assetName"></param>
        /// <param name="bundleName"></param>
        /// <returns></returns>
        public static ResData AllocateAsset(string assetName,string bundleName)
        {
            return Allocate(assetName,bundleName,ResType.Asset);
        }

        /// <summary>
        /// 分配对象
        /// </summary>
        /// <param name="assetName"></param>
        /// <param name="bundleName"></param>
        /// <param name="type"></param>
        /// <returns></returns>
        private static ResData Allocate(string assetName,string bundleName,ResType type)
        {
            var data = ObjectPool<ResData>.Instance.Allocate();
            data.Init(assetName,bundleName,type);
            return data;
        }
        public void Recycle()
        {
            ObjectPool<ResData>.Instance.Recycle(this);
        }

        public void Init(string assetName,string bundleName,ResType type)
        {
            m_bundleName = bundleName;
            m_assetName = assetName;
            m_type = type;
        }
    }
}