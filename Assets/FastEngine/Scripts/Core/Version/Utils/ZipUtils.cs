using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ICSharpCode.SharpZipLib.Zip;

namespace FastEngine
{

    public static class ZipUtils
    {
        #region ZipCallback
        public abstract class ZipCallback
        {
            /// <summary>
            ///  压缩单个文件或文件夹前执行的回调
            /// </summary>
            /// <param name="_entyr"></param>
            /// <returns>如果返回true,则压缩文件或文件夹，反之则不压缩文件或文件夹</returns>
            public virtual bool OnPreZip(ZipEntry _entry)
            {
                return true;
            }

            /// <summary>
            /// 压缩单个文件或文件夹后执行的回调
            /// </summary>
            /// <param name="_entry"></param>
            public virtual void OnPostZip(ZipEntry _entry) { }

            /// <summary>
            /// 压缩执行完毕后的回调
            /// </summary>
            /// <param name="_result"></param>
            public virtual void OnFinished(bool _result) { }
        }
        #endregion


    }
}

