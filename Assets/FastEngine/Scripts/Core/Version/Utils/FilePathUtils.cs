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
            if (Directory.Exists(directory))
                Directory.Delete(directory, true);
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

        /// <summary>
        /// 获取上级目录
        /// </summary>
        /// <param name="directory"></param>
        /// <param name="index"></param>
        /// <returns></returns>
        public static string GetTopDirectory(string directory, int index = 1)
        {
            directory = ReplaceSeparator(directory);
            char separator = Path.AltDirectorySeparatorChar;
            string[] ps = directory.Split(separator);

            if (ps.Length >= index)
            {
                string newDir = "";
                for (int i = 0; i < ps.Length- index; i++)
                {
                    newDir += ps[i] + Path.DirectorySeparatorChar;
                }

                return newDir;
            }
            return directory;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="paths"></param>
        /// <returns></returns>
        public static string Combine(params string[] paths)
        {
            switch (paths.Length)
            {
                case 2:
                    {
                        return ReplaceSeparator(Path.Combine(paths[0], paths[1]), "/");
                    }
                case 3:
                    {
                        return ReplaceSeparator(Path.Combine(paths[0], paths[1], paths[2]), "/");
                    }
                case 4:
                    {
                        return ReplaceSeparator(Path.Combine(paths[0], paths[1], paths[2], paths[3]), "/");
                    }
                default:
                    {
                        return ReplaceSeparator(Path.Combine(paths), "/");
                    }
            }
        }
        #endregion
    }
}

