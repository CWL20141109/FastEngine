using System.Collections;
using UnityEngine;

namespace FastEngine.Core
{
    public class AssetRes : Res, IPoolObject, IRunAsyncObject
    {

        private AssetBundleRequest m_request;
        private BundleRes m_bundleRes;

        private static AssetRes Allocate(ResData data)
        {
            var res = ObjectPool<AssetRes>.Instance.Allocate();
            res.Init(data);
            return res;
        }
        public void Init(ResData data)
        {
            m_bundleName = data.bundleName;
            m_assetName = data.assetName;
            m_state = ResState.Waiting;
            m_type = ResType.Asset;
            m_asset = null;
            m_asset = null;
        }

        public bool isRecycled { get; set; }

        public IEnumerator AsyncRun(IRunAsync async)
        {
            throw new System.NotImplementedException();
        }

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
            throw new System.NotImplementedException();
        }

        public override void Unload()
        {
            throw new System.NotImplementedException();
        }

        protected override void OnZeroRef()
        {
            throw new System.NotImplementedException();
        }
    }
}