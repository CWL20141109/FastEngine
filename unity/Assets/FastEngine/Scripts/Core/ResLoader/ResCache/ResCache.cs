using System.Security.Cryptography.X509Certificates;
using System.Collections.Generic;

namespace FastEngine.Core
{
    /// <summary>
    /// 资源池，缓存所有资源
    /// </summary>
    [MonoSingletonPath("FastEngine/ResLoader/ResCache")]
    public class ResCache : MonoSingleton<ResCache>
    {
        /// <summary>
        /// 资源池字典
        /// </summary>
        /// <typeparam name="string"></typeparam>
        /// <typeparam name="Res"></typeparam>
        /// <returns></returns>
        readonly Dictionary<string, Res> _resDictionary = new Dictionary<string, Res>();

        /// <summary>
        /// 获取Res对象
        /// </summary>
        /// <param name="data"></param>
        /// <param name="create"></param>
        /// <returns></returns>
        public Res _Get(ResData data, bool create = false)
        {
            Res res = null;
            if (_resDictionary.TryGetValue(data.poolkey, out res))
            {
                res.Retain();
                return res;
            }
            if (!create) return null;

            res = ResFactory.Create(data);

            if (res == null) return null;

            res.Retain();
            _resDictionary.Add(data.poolkey, res);

            return res;
        }

        public void _Remove(ResData data)
        {
            if (_resDictionary.ContainsKey(data.poolkey))
            {
                _resDictionary.Remove(data.poolkey);
            }
        }

        #region API
        /// <summary>
        /// 获取资源
        /// </summary>
        /// <param name="data"></param>
        /// <param name="createNew"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public static T Get<T>(ResData data, bool createNew = false) where T : Res
        {
            return Get(data, createNew) as T;
        }

        /// <summary>
        /// 获取资源
        /// </summary>
        /// <param name="data"></param>
        /// <param name="createNew"></param>
        /// <returns></returns>
        public static Res Get(ResData data, bool createNew = false)
        {
            return instance._Get(data, createNew);
        }
        /// <summary>
        /// 资源移除
        /// </summary>
        /// <param name="data"></param>
        public static void Remove(ResData data)
        {
            instance._Remove(data);
        }
        #endregion
    }
}