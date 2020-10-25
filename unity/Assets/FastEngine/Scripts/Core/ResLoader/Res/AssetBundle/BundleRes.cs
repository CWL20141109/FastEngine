using System.Collections;
using UnityEngine;

namespace FastEngine.Core
{
    /// <summary>
    /// AssetBundle 资源
    /// </summary>
    public class BundleRes : Res, IPoolObject
    {

        public AssetBundleCreateRequest m_request;

        private BundleRes[] m_dependencies;

        private int m_dependWaitCount;

        /// <summary>
        /// 分配对象
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public static BundleRes Allocate(ResData data)
        {
            var res = ObjectPool<BundleRes>.Instance.Allocate();
            
        }
        public bool isRecycled { get => throw new System.NotImplementedException(); set => throw new System.NotImplementedException(); }

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

        public void Init(ResData data)
        {
            m_bundleName = data.bundleName;
            m_state = ResState.Waiting;
            m_type = ResType.Bundle;
        }
    }
}