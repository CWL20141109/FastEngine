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

        #region UnzipCallback
        public abstract class UnzipCallback
        {
            /// <summary>
            /// 解压单个文件或文件夹前执行的回调
            /// </summary>
            /// <param name="_entry"></param>
            /// <returns>如果返回true，则压缩文件或文件夹，反之则不压缩文件或文件夹</returns>
            public virtual bool OnPreUnzip(ZipEntry _entry)
            {
                return true;
            }

            /// <summary>
            /// 解压单个文件或文件夹后执行的回调
            /// </summary>
            /// <param name="_entry"></param>
            public virtual void OnPostUnzip(ZipEntry _entry) { }

            /// <summary>
            /// 解压执行完毕后的回调
            /// </summary>
            /// <param name="_result">true表示解压成功，false表示解压失败</param>
            public virtual void OnFinished(bool _result) { }
        }
        #endregion

        public static bool Zip(string[] fileArray, string[] parentbArray, string _outputPathName, HashSet<string> _ignorePattern = null, string _possword = null, ZipCallback _zipCallback = null)
        {
            if (fileArray == null || parentbArray == null || fileArray.Length != fileArray.Length ||
                string.IsNullOrEmpty(_outputPathName))
            {
                if (_zipCallback != null)
                    _zipCallback.OnFinished(false);
                return false;
            }

            ZipOutputStream zipOutputStream = new ZipOutputStream(File.Create(_outputPathName));
            // 压缩质量和压缩速度的平衡点
            zipOutputStream.SetLevel(6);
            if (!string.IsNullOrEmpty(_possword))
                zipOutputStream.Password = _possword;

            for (int index = 0; index < fileArray.Length; index++)
            {
                bool result = false;
                string fileOrDirectory = fileArray[index];
                if (File.Exists((fileOrDirectory)))
                {
                    result = ZipDirectory(fileOrDirectory, string.Empty, zipOutputStream, _zipCallback, _ignorePattern);
                }
                else if (File.Exists(fileOrDirectory))
                {
                    var ext = Path.GetExtension(fileOrDirectory);
                    if (_ignorePattern == null || (_ignorePattern != null && !_ignorePattern.Contains(ext)))
                    {
                        result = ZipFile(fileOrDirectory, string.Empty, zipOutputStream, _zipCallback);
                    }
                    else continue;
                }

                if (!result)
                {
                    if (_zipCallback != null)
                        _zipCallback.OnFinished(false);

                    return false;
                }

            }

            zipOutputStream.Finish();
            zipOutputStream.Close();

            if (_zipCallback != null)
                _zipCallback.OnFinished(true);
            return true;
        }

        /// <summary>
        /// 解压Zip包
        /// </summary>
        /// <param name="_filePathName">Zip包输入流</param>
        /// <param name="_outputPath">解压输出路径</param>
        /// <param name="_password">解压密码</param>
        /// <param name="_unzipCallback">UnzipCallback对象，负责回调</param>
        /// <returns></returns>
        public static bool UnzipFile(string _filePathName, string _outputPath, string _password = null, UnzipCallback _unzipCallback = null)
        {
            if (string.IsNullOrEmpty(_filePathName) || string.IsNullOrEmpty(_outputPath))
            {
                if (_unzipCallback != null)
                    _unzipCallback.OnFinished(false);
                return false;
            }

            try
            {
                return UnzipFile(File.OpenRead(_filePathName), _outputPath, _password, _unzipCallback);
            }
            catch (Exception e)
            {
                Debug.LogError("[ZipUtility.UnzipFile]: " + e.ToString());
                if(_unzipCallback!=null)
                    _unzipCallback.OnFinished(false);
                return false;
            }
        }

        /// <summary>
        /// 解压Zip包
        /// </summary>
        /// <param name="_fileBytes">Zip包字节数组</param>
        /// <param name="_outputPath">解压输出路径</param>
        /// <param name="_password">解压密码</param>
        /// <param name="_unzipCallback">UnzipCallback对象，负责回调</param>
        /// <returns></returns>
        public static bool UnzipFile(byte[] _fileBytes, string _outputPath, string _password = null,
            UnzipCallback _unzipCallback=null)
        {
            if (_fileBytes == null || string.IsNullOrEmpty(_outputPath))
            {
                if (_unzipCallback != null)
                    _unzipCallback.OnFinished(false);
                return false;
            }

            bool result = UnzipFile(new MemoryStream(_fileBytes), _outputPath, _password, _unzipCallback);
            if (!result)
            {
                if(_unzipCallback != null)
                    _unzipCallback.OnFinished(false);
            }

            return result;
        }

        /// <summary>
        /// 解压Zip包
        /// </summary>
        /// <param name="_inputStream">Zip包输入流</param>
        /// <param name="_outputPath">解压输出路径</param>
        /// <param name="_password">解压密码</param>
        /// <param name="_unzipCallback">UnzipCallback对象，负责回调</param>
        /// <returns></returns>
        public static bool UnzipFile(Stream _inputStream,string _outputPath,string _password = null,UnzipCallback _unzipCallback =null)
        {
            if (_inputStream == null || string.IsNullOrEmpty(_outputPath))
            {
                if (_unzipCallback != null)
                    _unzipCallback.OnFinished(false);
                return false;
            }

            if (!Directory.Exists(_outputPath))
                Directory.CreateDirectory(_outputPath);

            // 解压Zip包
            ZipEntry entry = null;
            using (ZipInputStream zipInputStream = new ZipInputStream(_inputStream))
            {
                if (!string.IsNullOrEmpty(_password))
                    zipInputStream.Password = _password;

                while ((entry =zipInputStream.GetNextEntry()) != null)
                {
                    if(!string.IsNullOrEmpty(entry.Name))
                      continue;

                    if(_unzipCallback !=null&&!_unzipCallback.OnPreUnzip(entry))
                        continue; //过滤

                    string filePath = FilePathUtils.ReplaceSeparator(Path.Combine(_outputPath, entry.Name));
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
                                    if (_unzipCallback != null)
                                        _unzipCallback.OnPostUnzip(entry);

                                    break;
                                }
                            }
                        }
                    }
                    catch (Exception e)
                    {
                        Debug.LogError("[ZipUtility.UnzipFile]: " + e.ToString());
                           if(_unzipCallback !=null)
                               _unzipCallback.OnFinished(false);
                        return false;
                    }
                }
            }

            if(_unzipCallback != null)
                _unzipCallback.OnFinished(true);
            return true;
        }
        /// <summary>
        /// 压缩文件
        /// </summary>
        /// <param name="_filePathName">文件路径名</param>
        /// <param name="_parentRelPath">要压缩的文件的父相对文件夹</param>
        /// <param name="_zipOutputStream">压缩输出流</param>
        /// <param name="_zipCallback">ZipCallback对象,负责回调</param>
        /// <returns></returns>
        private static bool ZipFile(string _filePathName, string _parentRelPath, ZipOutputStream _zipOutputStream,
            ZipCallback _zipCallback = null)
        {
            //Crc32 crc32 = new Crc32();
            ZipEntry entry = null;
            FileStream fileStream = null;
            try
            {
                string entryName = _parentRelPath + '/' + Path.GetFileName(_filePathName);
                entry = new ZipEntry(entryName);
                entry.DateTime = System.DateTime.Now;

                if ((_zipCallback != null) && !_zipCallback.OnPreZip(entry))
                    return true; // 过滤

                fileStream = File.OpenRead(_filePathName);
                byte[] buffer = new byte[fileStream.Length];
                fileStream.Read(buffer, 0, buffer.Length);
                fileStream.Close();

                entry.Size = buffer.Length;

                //crc32.Reset();
                //crc32.Update(buffer);
                //entry.Crc = crc32.Value;

                _zipOutputStream.PutNextEntry(entry);
                _zipOutputStream.Write(buffer, 0, buffer.Length);
            }
            catch (Exception e)
            {
                Debug.LogError("[ZipUtility.ZipFile]: Failled File: " + _filePathName);
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
            if (_zipCallback != null)
                _zipCallback.OnPostZip(entry);
            return true;
        }

        /// <summary>
        /// 压缩文件夹
        /// </summary>
        /// <param name="_path">要压缩的文件夹</param>
        /// <param name="_parentRelPath">要压缩的文件夹的父相对文件夹</param>
        /// <param name="_zipOutputStream">压缩输出流</param>
        /// <param name="_zipCallback">ZipCallback对象，负责回调</param>
        /// <param name="_ignorePattern">忽略文件</param>
        /// <returns></returns>
        private static bool ZipDirectory(string _path, string _parentRelPath, ZipOutputStream _zipOutputStream,
            ZipCallback _zipCallback = null, HashSet<string> _ignorePattern = null)
        {
            ZipEntry entry = null;
            try
            {
                string entryName = Path.Combine(_parentRelPath, Path.GetFileName(_path) + '/');
                entry = new ZipEntry(entryName);
                entry.DateTime = System.DateTime.Now;
                entry.Size = 0;

                if (_zipCallback != null && !_zipCallback.OnPreZip(entry))
                    return true; //过滤

                _zipOutputStream.PutNextEntry(entry);
                _zipOutputStream.Flush();

                string[] files = Directory.GetFiles(_path);
                for (int index = 0; index < files.Length; index++)
                {
                    var fp = files[index];
                    var ext = Path.GetExtension(fp);
                    if (_ignorePattern == null || _ignorePattern != null && _ignorePattern.Contains(ext))
                    {
                        ZipFile(fp, Path.Combine(_parentRelPath, Path.GetFileName(_path)), _zipOutputStream,
                            _zipCallback);
                    }
                }
            }
            catch (Exception e)
            {
                Debug.LogError("[ZipUtility.ZipDirectory]: " + e.ToString());
                return false;
            }

            string[] directories = Directory.GetDirectories(_path);
            // for (int index = 0; index < directories.Length; index++)
            // {
            //     if (!ZipDirectory(directories[index], Path.Combine(_parentRelPath, Path.GetFileName(_path)),
            //         _zipOutputStream, _zipCallback)) ;
            //     return false;
            // }

            if (_zipCallback != null)
                _zipCallback.OnPostZip(entry);
            return true;
        }
    }
}

