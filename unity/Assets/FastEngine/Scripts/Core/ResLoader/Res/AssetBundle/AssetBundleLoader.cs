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
		protected BundleRes m_bundleRes;
		public BundleRes bundleres { get { return m_bundleRes; } }

		/// <summary>
		/// asset 资源
		/// </summary>
		protected AssetRes m_assetRes;
		public AssetRes assetRes { get { return m_assetRes; } }
		/// <summary>
		/// 只加载bundle
		/// </summary>
		protected bool m_only;

		public static AssetBundleLoader Allocate(string resPath, ResNotificationListener listener)
		{
			var mapping = AssetBundleDB.GetMappingData(resPath);
			var loader = ObjectPool<AssetBundleLoader>.Instance.Allocate();
			loader.Init(mapping.bundleName, mapping.assetName, listener);
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
		public bool isRecycled { get; set; }

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
			m_only = string.IsNullOrEmpty(assetName);

			m_bundleRes = ResCache.Get<BundleRes>(ResData.AlloocateBundle(bundleName), true);

			if (!m_only)
				m_assetRes = ResCache.Get<AssetRes>(ResData.AllocateAsset(assetName, bundleName), true);

			m_listener = listener;

		}

		public override void Unload()
		{
			throw new System.NotImplementedException();
		}

		protected override void OnReceiveNotification(bool ready, Res Res)
		{
			throw new System.NotImplementedException();
		}
        #endregion
	}
}