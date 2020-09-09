using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Text;
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
        /// 路径连接
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

        /// <summary>
        /// 获取路径上的第几个位置内容
        /// </summary>
        /// <param name="path"></param>
        /// <param name="index"></param>
        /// <returns></returns>
        public static string GetPathSction(string path, int index)
        {
            if (index == 0)
                return "";

            path = ReplaceSeparator(path);
            char separator = Path.AltDirectorySeparatorChar;
            string[] ps = path.Split(separator);

            if (index < 0)
            {
                index = ps.Length + index + 1;
            }

            if (ps.Length >= index)
            {
                return ps[index - 1];
            }
            return "";
        }
        #endregion

        #region 文件
        /// <summary>
        /// 写入文件
        /// </summary>
        /// <param name="path"></param>
        /// <param name="context"></param>
        /// <returns></returns>
        public static bool FileWriteAllText(string path, string context)
        {
            try
            {
                FileInfo info = new FileInfo(path);
                if (!info.Directory.Exists) info.Directory.Create();
                if(info.Exists)info.Delete();

                File.WriteAllText(path,context);
            }
            catch (Exception e)
            {
                Debug.LogError("FileWriteAllText Exception: " + e.ToString());
                return false;
            }
            return true;
        }

        /// <summary>
        /// 写入文件
        /// </summary>
        /// <param name="path"></param>
        /// <param name="context"></param>
        /// <param name="encoding"></param>
        /// <returns></returns>
        public static bool FileWriteAllText(string path, string context, Encoding encoding)
        {
            try
            {
                FileInfo info = new FileInfo(path);
                if(!info.Directory.Exists)info.Directory.Create();
                if(info.Exists) info.Delete();

                File.WriteAllText(path,context);
            }
            catch (Exception e)
            {
                Debug.LogError("FileWriteAllText Exception: " + e.ToString());
                return false;
            }
            return true;
        }

        /// <summary>
        /// 读取文件
        /// </summary>
        /// <param name="path"></param>
        /// <param name="succeed"></param>
        /// <returns></returns>
        public static string FileReadAllText(string path, out bool succeed)
        {
            try
            {
                FileInfo info = new FileInfo(path);
                if (info.Exists)
                {
                    succeed = true;
                    return File.ReadAllText(path);
                }
            }
            catch (Exception e)
            {
                Debug.LogError("FileReadAllText Exception: " + e.ToString());
            }

            succeed = false;
            return "";
        }

        /// <summary>
        /// 删除文件
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        public static bool DeleteFile(string path)
        {
            try
            {
                if (File.Exists(path))
                {
                    File.Delete(path);
                    return true;
                }
            }
            catch (Exception e)
            {
                Debug.LogError("DeleteFile Exception: " + e.ToString());
            }

            return false;
        }

        /// <summary>
        /// 文件是否存在
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        public static bool FileExists(string path)
        {
            return File.Exists(path);
        }

        /// <summary>
        /// 文件复制
        /// </summary>
        /// <param name="source"></param>
        /// <param name="dest"></param>
        /// <returns></returns>
        public static bool FileCopy(string source, string dest)
        {
            try
            {
                FileInfo info = new FileInfo(dest);
                if (!Directory.Exists(info.DirectoryName))
                {
                    Directory.CreateDirectory(info.DirectoryName);
                }

                DeleteFile(dest);
                File.Copy(source,dest);
            }
            catch (Exception e)
            {
                Debug.LogError("FileCopy Exception: " + e.ToString());
                return false;
            }

            return true;
        }

        /// <summary>
        /// 文件大小
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        public static long FileSize(string path)
        {
            return (new FileInfo(path)).Length;
        }
        #endregion
    }
}

