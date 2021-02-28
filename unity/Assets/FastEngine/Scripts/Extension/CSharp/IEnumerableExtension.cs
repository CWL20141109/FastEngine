using System;
using System.Collections.Generic;

namespace FastEngine
{
    public static class IEnumerableExtension
    {
        #region Array Extension
        /// <summary>
        /// 数组排序
        /// </summary>
        /// <param name="selfArray"></param>
        /// <param name="action"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public static T[] ForEach<T>(this T[] selfArray, Action<T> action)
        {
            Array.ForEach(selfArray, action);
            return selfArray;
        }

        /// <summary>
        /// 字典遍历
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="selfArray"></param>
        /// <param name="action"></param>
        /// <returns></returns>
        public static IEquatable<T> ForEach<T>(this IEnumerable<T> selfArray, Action<T> action)
        {
            if (action == null) throw new ArgumentException();
            foreach (var item in selfArray)
            {
                action(item);
            }
            return null;
        }
        #endregion
    }
}