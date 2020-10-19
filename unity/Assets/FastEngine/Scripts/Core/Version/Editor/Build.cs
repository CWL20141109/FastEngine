using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace FastEngine.Editor.Version
{

    public class Build
    {
        /// <summary>
        /// 压缩回调
        /// </summary>
        class ZipCallback:ZipUtils.ZipCallback
        {
            /// <summary>
            /// 压缩文件数量
            /// </summary>
            public int count { get; private set; }

            public override void OnPostZip(ICSharpCode.SharpZipLib.Zip.ZipEntry _entry)
            {
                count++;
            }

            /// <summary>
            /// 资源压缩回调
            /// </summary>
            private static ZipCallback zipCallback;

         
        }
    }
}

