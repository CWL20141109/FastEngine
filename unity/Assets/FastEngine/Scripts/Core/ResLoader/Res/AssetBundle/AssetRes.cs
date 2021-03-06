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
            var res = ObjectPool<AssetRes>.instance.Allocate();
            res.Init(data);
            return res;
        }
        
        public void Init(ResData data)
        {
            mBundleName = data.bundleName;
            mAssetName = data.assetName;
            mState = ResState.Waiting;
            mType = ResType.Asset;
            mAsset = null;
            mAsset = null;
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
            ResCache.Remove(ResData.AllocateAsset(assetName,bundleName));
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