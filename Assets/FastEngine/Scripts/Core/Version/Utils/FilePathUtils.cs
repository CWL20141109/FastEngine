using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

namespace FastEngine
{

    public static class FilePathUtils
    {
        #region 路径
        /// <summary>
        /// 创建目录
        /// </summary>
        /// <param name="directory">路径</param>
        public static void DirectoryCreate(string directory)
        {
            if (!Directory.Exists(directory))
                Directory.CreateDirectory(directory);
        }

        /// <summary>
        /// 删除目录
        /// </summary>
        /// <param name="directory">路径</param>
        public static void DirectoryDelete(string directory)
        {
            if(Directory.Exists(directory))
                Directory.Delete(directory,true);
        }

        /// <summary>
        /// 替换路径分割符
        /// </summary>
        /// <param name="path">路径</param>
        /// <param name="separator">分隔符</param>
        /// <returns></returns>
        public static string ReplaceSeparator(string path, string separator = "")
        {
            if (string.IsNullOrEmpty(separator))
            {
                separator = Path.AltDirectorySeparatorChar.ToString();
            }

            return path.Replace("\\", separator).Replace("//", separator);
        }
        #endregion
    }
}

