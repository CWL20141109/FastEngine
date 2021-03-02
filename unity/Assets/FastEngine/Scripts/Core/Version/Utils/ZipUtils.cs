using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
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
            public virtual bool OnPreZip(ZipEntry entry)
            {
                return true;
            }

            /// <summary>
            /// 压缩单个文件或文件夹后执行的回调
            /// </summary>
            /// <param name="entry"></param>
            public virtual void OnPostZip(ZipEntry entry) { }

            /// <summary>
            /// 压缩执行完毕后的回调
            /// </summary>
            /// <param name="result"></param>
            public virtual void OnFinished(bool result) { }
        }
        #endregion

        #region UnzipCallback
        public abstract class UnzipCallback
        {
            /// <summary>
            /// 解压单个文件或文件夹前执行的回调
            /// </summary>
            /// <param name="entry"></param>
            /// <returns>如果返回true，则压缩文件或文件夹，反之则不压缩文件或文件夹</returns>
            public virtual bool OnPreUnzip(ZipEntry entry)
            {
                return true;
            }

            /// <summary>
            /// 解压单个文件或文件夹后执行的回调
            /// </summary>
            /// <param name="entry"></param>
            public virtual void OnPostUnzip(ZipEntry entry) { }

            /// <summary>
            /// 解压执行完毕后的回调
            /// </summary>
            /// <param name="result">true表示解压成功，false表示解压失败</param>
            public virtual void OnFinished(bool result) { }
        }
        #endregion

        public static bool Zip(string[] fileArray, string[] parentbArray, string outputPathName, HashSet<string> ignorePattern = null, string possword = null, ZipCallback zipCallback = null)
        {
            if (fileArray == null || parentbArray == null || fileArray.Length != fileArray.Length ||
                string.IsNullOrEmpty(outputPathName))
            {
                if (zipCallback != null)
                    zipCallback.OnFinished(false);
                return false;
            }

            ZipOutputStream zipOutputStream = new ZipOutputStream(File.Create(outputPathName));
            // 压缩质量和压缩速度的平衡点
            zipOutputStream.SetLevel(6);
            if (!string.IsNullOrEmpty(possword))
                zipOutputStream.Password = possword;

            for (int index = 0; index < fileArray.Length; index++)
            {
                bool result = false;
                string fileOrDirectory = fileArray[index];
                if (File.Exists((fileOrDirectory)))
                {
                    result = ZipDirectory(fileOrDirectory, string.Empty, zipOutputStream, zipCallback, ignorePattern);
                }
                else if (File.Exists(fileOrDirectory))
                {
                    var ext = Path.GetExtension(fileOrDirectory);
                    if (ignorePattern == null || (ignorePattern != null && !ignorePattern.Contains(ext)))
                    {
                        result = ZipFile(fileOrDirectory, string.Empty, zipOutputStream, zipCallback);
                    }
                    else continue;
                }

                if (!result)
                {
                    if (zipCallback != null)
                        zipCallback.OnFinished(false);

                    return false;
                }

            }

            zipOutputStream.Finish();
            zipOutputStream.Close();

            if (zipCallback != null)
                zipCallback.OnFinished(true);
            return true;
        }

        /// <summary>
        /// 解压Zip包
        /// </summary>
        /// <param name="filePathName">Zip包输入流</param>
        /// <param name="outputPath">解压输出路径</param>
        /// <param name="password">解压密码</param>
        /// <param name="unzipCallback">UnzipCallback对象，负责回调</param>
        /// <returns></returns>
        public static bool UnzipFile(string filePathName, string outputPath, string password = null, UnzipCallback unzipCallback = null)
        {
            if (string.IsNullOrEmpty(filePathName) || string.IsNullOrEmpty(outputPath))
            {
                if (unzipCallback != null)
                    unzipCallback.OnFinished(false);
                return false;
            }

            try
            {
                return UnzipFile(File.OpenRead(filePathName), outputPath, password, unzipCallback);
            }
            catch (Exception e)
            {
                Debug.LogError("[ZipUtility.UnzipFile]: " + e.ToString());
                if(unzipCallback!=null)
                    unzipCallback.OnFinished(false);
                return false;
            }
        }

        /// <summary>
        /// 解压Zip包
        /// </summary>
        /// <param name="fileBytes">Zip包字节数组</param>
        /// <param name="outputPath">解压输出路径</param>
        /// <param name="password">解压密码</param>
        /// <param name="unzipCallback">UnzipCallback对象，负责回调</param>
        /// <returns></returns>
        public static bool UnzipFile(byte[] fileBytes, string outputPath, string password = null,
            UnzipCallback unzipCallback=null)
        {
            if (fileBytes == null || string.IsNullOrEmpty(outputPath))
            {
                if (unzipCallback != null)
                    unzipCallback.OnFinished(false);
                return false;
            }

            bool result = UnzipFile(new MemoryStream(fileBytes), outputPath, password, unzipCallback);
            if (!result)
            {
                if(unzipCallback != null)
                    unzipCallback.OnFinished(false);
            }

            return result;
        }

        /// <summary>
        /// 解压Zip包
        /// </summary>
        /// <param name="inputStream">Zip包输入流</param>
        /// <param name="outputPath">解压输出路径</param>
        /// <param name="password">解压密码</param>
        /// <param name="unzipCallback">UnzipCallback对象，负责回调</param>
        /// <returns></returns>
        public static bool UnzipFile(Stream inputStream,string outputPath,string password = null,UnzipCallback unzipCallback =null)
        {
            if (inputStream == null || string.IsNullOrEmpty(outputPath))
            {
                if (unzipCallback != null)
                    unzipCallback.OnFinished(false);
                return false;
            }

            if (!Directory.Exists(outputPath))
                Directory.CreateDirectory(outputPath);

            // 解压Zip包
            ZipEntry entry = null;
            using (ZipInputStream zipInputStream = new ZipInputStream(inputStream))
            {
                if (!string.IsNullOrEmpty(password))
                    zipInputStream.Password = password;

                while ((entry =zipInputStream.GetNextEntry()) != null)
                {
                    if(!string.IsNullOrEmpty(entry.Name))
                      continue;

                    if(unzipCallback !=null&&!unzipCallback.OnPreUnzip(entry))
                        continue; //过滤

                    string filePath = FilePathUtils.ReplaceSeparator(Path.Combine(outputPath, entry.Name));
                    // 创建文件目录
                    if (entry.IsDirectory)
                    {
                        Directory.CreateDirectory(Path.GetDirectoryName(filePath));
                        continue; 
                    }
                    //写入文件
                    try
                    {
                        using (FileStream fileStream = File.Create(filePath))
                        {
                            byte[] bytes =new byte[1024];
                            while (true)
                            {
                                int count = zipInputStream.Read(bytes, 0, bytes.Length);
                                if (count > 0)
                                {
                                    fileStream.Write(bytes,0,count);
                                }
                                else
                                {
                                    if (unzipCallback != null)
                                        unzipCallback.OnPostUnzip(entry);

                                    break;
                                }
                            }
                        }
                    }
                    catch (Exception e)
                    {
                        Debug.LogError("[ZipUtility.UnzipFile]: " + e.ToString());
                           if(unzipCallback !=null)
                               unzipCallback.OnFinished(false);
                        return false;
                    }
                }
            }

            if(unzipCallback != null)
                unzipCallback.OnFinished(true);
            return true;
        }
        /// <summary>
        /// 压缩文件
        /// </summary>
        /// <param name="filePathName">文件路径名</param>
        /// <param name="parentRelPath">要压缩的文件的父相对文件夹</param>
        /// <param name="zipOutputStream">压缩输出流</param>
        /// <param name="zipCallback">ZipCallback对象,负责回调</param>
        /// <returns></returns>
        private static bool ZipFile(string filePathName, string parentRelPath, ZipOutputStream zipOutputStream,
            ZipCallback zipCallback = null)
        {
            //Crc32 crc32 = new Crc32();
            ZipEntry entry = null;
            FileStream fileStream = null;
            try
            {
                string entryName = parentRelPath + '/' + Path.GetFileName(filePathName);
                entry = new ZipEntry(entryName);
                entry.DateTime = System.DateTime.Now;

                if ((zipCallback != null) && !zipCallback.OnPreZip(entry))
                    return true; // 过滤

                fileStream = File.OpenRead(filePathName);
                byte[] buffer = new byte[fileStream.Length];
                fileStream.Read(buffer, 0, buffer.Length);
                fileStream.Close();

                entry.Size = buffer.Length;

                //crc32.Reset();
                //crc32.Update(buffer);
                //entry.Crc = crc32.Value;

                zipOutputStream.PutNextEntry(entry);
                zipOutputStream.Write(buffer, 0, buffer.Length);
            }
            catch (Exception e)
            {
                Debug.LogError("[ZipUtility.ZipFile]: Failled File: " + filePathName);
                Debug.LogError("[ZipUtility.ZipFile]: " + e.ToString());
                return false;
            }
            finally
            {
                if (fileStream != null)
                {
                    fileStream.Close();
                    fileStream.Dispose();
                }
            }
            if (zipCallback != null)
                zipCallback.OnPostZip(entry);
            return true;
        }

        /// <summary>
        /// 压缩文件夹
        /// </summary>
        /// <param name="path">要压缩的文件夹</param>
        /// <param name="parentRelPath">要压缩的文件夹的父相对文件夹</param>
        /// <param name="zipOutputStream">压缩输出流</param>
        /// <param name="zipCallback">ZipCallback对象，负责回调</param>
        /// <param name="ignorePattern">忽略文件</param>
        /// <returns></returns>
        private static bool ZipDirectory(string path, string parentRelPath, ZipOutputStream zipOutputStream,
            ZipCallback zipCallback = null, HashSet<string> ignorePattern = null)
        {
            ZipEntry entry = null;
            try
            {
                string entryName = Path.Combine(parentRelPath, Path.GetFileName(path) + '/');
                entry = new ZipEntry(entryName);
                entry.DateTime = System.DateTime.Now;
                entry.Size = 0;

                if (zipCallback != null && !zipCallback.OnPreZip(entry))
                    return true; //过滤

                zipOutputStream.PutNextEntry(entry);
                zipOutputStream.Flush();

                string[] files = Directory.GetFiles(path);
                for (int index = 0; index < files.Length; index++)
                {
                    var fp = files[index];
                    var ext = Path.GetExtension(fp);
                    if (ignorePattern == null || ignorePattern != null && ignorePattern.Contains(ext))
                    {
                        ZipFile(fp, Path.Combine(parentRelPath, Path.GetFileName(path)), zipOutputStream,
                            zipCallback);
                    }
                }
            }
            catch (Exception e)
            {
                Debug.LogError("[ZipUtility.ZipDirectory]: " + e.ToString());
                return false;
            }

            string[] directories = Directory.GetDirectories(path);
            // for (int index = 0; index < directories.Length; index++)
            // {
            //     if (!ZipDirectory(directories[index], Path.Combine(_parentRelPath, Path.GetFileName(_path)),
            //         _zipOutputStream, _zipCallback)) ;
            //     return false;
            // }

            if (zipCallback != null)
                zipCallback.OnPostZip(entry);
            return true;
        }
    }
}

