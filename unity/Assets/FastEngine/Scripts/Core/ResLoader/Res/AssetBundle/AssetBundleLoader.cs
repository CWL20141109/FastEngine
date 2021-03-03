namespace FastEngine.Core
{
	/// <summary>
	/// 资源加载器
	/// </summary>
	public class AssetBundleLoader : ResLoader, IPoolObject
	{

		// /// <summary>
		// ///  资源名称
		// /// </summary>
		// protected string m_assetName;
		// /// <summary>
		// ///  资源名称
		// /// </summary>
		// public string assetName { get { return m_assetName; } }
		//
		// /// <summary>
		// /// bundle名称
		// /// </summary>
		// protected string m_bundleName;
		// /// <summary>
		// /// bundle名称
		// /// </summary>
		// public string bundleName { get { return m_bundleName; } }

		/// <summary>
		/// bundle 资源
		/// </summary>
		protected BundleRes MBundleRes;
		public BundleRes Bundleres { get { return MBundleRes; } }

		/// <summary>
		/// asset 资源
		/// </summary>
		protected AssetRes MAssetRes;
		public AssetRes AssetRes { get { return MAssetRes; } }
		/// <summary>
		/// 只加载bundle
		/// </summary>
		protected bool MOnly;

		public static AssetBundleLoader Allocate(string resPath, ResNotificationListener listener)
		{
			var mapping = AssetBundleDB.GetMappingData(resPath);
			var loader = ObjectPool<AssetBundleLoader>.Instance.Allocate();
			loader.Init(mapping.BundleName, mapping.AssetName, listener);
			return loader;
		}

		public static AssetBundleLoader Allocate(string bundleName, string assetName, ResNotificationListener listener)
		{
			var loader = ObjectPool<AssetBundleLoader>.Instance.Allocate();
			loader.Init(bundleName, assetName, listener);
			return loader;
		}


        #region IPoolObject
		/// <summary>
		/// 回收标识
		/// </summary>
		/// <value></value>
		public bool IsRecycled { get; set; }

		public override bool LoadAsync()
		{
			throw new System.NotImplementedException();
		}

		public override bool LoadSync()
		{
			throw new System.NotImplementedException();
		}

		public void Recycle()
		{
			ObjectPool<AssetBundleLoader>.Instance.Recycle(this);
		}

		public void Init(string bundleName, string assetName, ResNotificationListener listener)
		{
			MOnly = string.IsNullOrEmpty(assetName);

			MBundleRes = ResCache.Get<BundleRes>(ResData.AlloocateBundle(bundleName), true);

			if (!MOnly)
				MAssetRes = ResCache.Get<AssetRes>(ResData.AllocateAsset(assetName, bundleName), true);

			MListener = listener;

		}

		public override void Unload()
		{
			throw new System.NotImplementedException();
		}

		protected override void OnReceiveNotification(bool ready, Res res)
		{
			throw new System.NotImplementedException();
		}
        #endregion
	}
}