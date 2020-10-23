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

        // public Res _Get(ResData data, bool creat = false)
        // {
        //     Res res = null;

        //     return res;
        // }

        public void test()
        {

        }
    }
}