namespace FastEngine.Core
{
    public class ResData : IPoolObject
    {
        /// <summary>
        ///  资源名称
        /// </summary>
        protected string mAssetName;
        /// <summary>
        ///  资源名称
        /// </summary>
        public string AssetName { get { return mAssetName; } }

        /// <summary>
        /// bundle名称
        /// </summary>
        protected string mBundleName;
        /// <summary>
        /// bundle名称
        /// </summary>
        public string BundleName { get { return mBundleName; } }

        /// <summary>
        /// 资源类型
        /// </summary>
        private ResType _mType;
        /// <summary>
        /// 资源类型
        /// </summary>
        public ResType Type { get { return _mType; } }

        public string Poolkey
        {
            get
            {
                switch (_mType)
                {
                    case ResType.Resource:
                        return mAssetName.ToLower();
                    case ResType.Bundle:
                        return mBundleName.ToLower();
                    case ResType.Asset:
                        return (mBundleName + mAssetName).ToLower();
                    default:
                        return "";
                }
            }
        }

        public bool IsRecycled { get => throw new System.NotImplementedException(); set => throw new System.NotImplementedException(); }
        
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
            mBundleName = bundleName;
            mAssetName = assetName;
            _mType = type;
        }
    }
}