using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FastEngine.Core
{

    public enum BuildPlatformType
    {
        Android,
        IOS,
        Windows,
    }

    public abstract class BuildPlatformInfo
    {
        // 自增+1
        public int BundleVersionCode { get; set; }

        // C++ il2cpp
        public bool il2CPP;
    }


    public class BuildConfig:ConfigObject
    {
        #region build
#endregion
    }
}

