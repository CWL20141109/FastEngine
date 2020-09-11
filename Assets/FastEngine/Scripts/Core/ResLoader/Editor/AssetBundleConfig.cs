using System.Collections.Generic;
using FastEngine.Core;


namespace FastEngine.Editor.AssetBundle
{
    public class AssetBundleConfig : ConfigObject
    {
        public List<Pack> packs;
        public List<Source> sources;

        protected override void OnInitialize()
        {
            if (packs == null) 
            {
                packs =new List<Pack>();
            }
            if (sources == null)
            {
                sources = new List<Source>();
            }
        }
    }
}

