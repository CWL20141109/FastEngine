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

        public static bool Zip(string[] fileArray, string[] parentbArray, string _outputPathName,string _possword = null, ZipCallback _zipCallback = null)
        {
            if (fileArray == null || parentbArray == null || fileArray.Length != fileArray.Length ||
                string.IsNullOrEmpty(_outputPathName))
            {
                if(_zipCallback !=null)
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
                    result = ZipFile()                }
                    
            }
            if (_zipCallback != null)
                _zipCallback.OnFinished(true);
            return true;
        }

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

                if ((_zipCallback != null) && _zipCallback.OnPreZip(entry))
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
            if(_zipCallback != null)
                _zipCallback.OnPostZip(entry);
            return true;
        }

    }
}

