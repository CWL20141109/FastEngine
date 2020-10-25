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
        readonly Dictionary<string, Res> resDictionary = new Dictionary<string, Res>();

        /// <summary>
        /// 获取Res对象
        /// </summary>
        /// <param name="data"></param>
        /// <param name="creat"></param>
        /// <returns></returns>
        public Res _Get(ResData data, bool creat = false)
        {
            Res res = null;
            if (resDictionary.TryGetValue(data.poolkey, out res))
            {
                res.Retain();
                return res;
            }
            if (!creat) return null;

            res = ResFactory.Create(data);

            if (res == null) return null;

            res.Retain();
            resDictionary.Add(data.poolkey, res);

            return res;
        }

        public void _Remove(ResData data)
        {
            if(resDictionary.ContainsKey(data.poolkey))
            {
                resDictionary.Remove(data.poolkey);
            }
        }
    }
}