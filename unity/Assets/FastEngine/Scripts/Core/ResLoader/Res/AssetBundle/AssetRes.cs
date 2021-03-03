using System.Collections;
using UnityEngine;

namespace FastEngine.Core
{
    public class AssetRes : Res, IPoolObject, IRunAsyncObject
    {

        private AssetBundleRequest _mRequest;
        private BundleRes _mBundleRes;

        public static AssetRes Allocate(ResData data)
        {
            var res = ObjectPool<AssetRes>.Instance.Allocate();
            res.Init(data);
            return res;
        }
        
        public void Init(ResData data)
        {
            MBundleName = data.BundleName;
            MAssetName = data.AssetName;
            MState = ResState.Waiting;
            MType = ResType.Asset;
            MAsset = null;
            MAsset = null;
        }

        public bool IsRecycled { get; set; }

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
            ResCache.Remove(ResData.AllocateAsset(AssetName,BundleName));
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