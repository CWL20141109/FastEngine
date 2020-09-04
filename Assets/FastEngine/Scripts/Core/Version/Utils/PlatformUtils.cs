using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FastEngine
{
    public class PlatformUtils
    {
        /// <summary>
        /// 平台标识Id
        /// </summary>
        public static string PlatformId()
        {
#if UNITY_ANDROID
            return "Android";
#elif UNITY_IOS
            return "iOS";
#elif UNITY_STANDALONE_WIN
            return "Windows";
#elif UNITY_STANDALONE_OSX
            return "OSX";
#else
            return "Unknown";
#endif
        }
    }
}