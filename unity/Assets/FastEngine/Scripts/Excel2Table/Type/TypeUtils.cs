/*
* @Author: 
* @Description:
* @Date: 2021-02-28 16:51:18
*/

using System;
using UnityEngine;
namespace FastEngine.Core.Excel2Table
{
    public class TypeUtils
    {
        private static readonly char[] Separator = new char[] { ',' };

        /// <summary>
        /// type string to field type
        /// </summary>
        /// <param name="ts"></param>
        /// <returns></returns>
        public static FieldType TypeContentToFieldType(string ts)
        {
            for (int i = 0; i < (int)FieldType.Unknow; i++)
            {
                var fieldType = (FieldType)i;
                if (fieldType.ToString().Equals(ts))
                {
                    return fieldType;
                }
            }
            return FieldType.Unknow;
        }

        public static string FieldTypeToTypeContent(FieldType fieldType)
        {
            switch (fieldType)
            {
                case FieldType.Byte:
                    return "byte";
                case FieldType.Int:
                    return "int";
                case FieldType.Long:
                    return "long";
                case FieldType.Float:
                    return "float";
                case FieldType.Double:
                    return "double";
                case FieldType.String:
                    return "string";
                case FieldType.Boolean:
                    return "i18nObject";
                case FieldType.Vector2:
                    return "Vector2";
                case FieldType.Vector3:
                    return "Vector3";
                case FieldType.I18N:
                    return "i18nObject";
                case FieldType.ArrayByte:
                    return "byte[]";
                case FieldType.ArrayInt:
                    return "int[]";
                case FieldType.ArrayLong:
                    return "long[]";
                case FieldType.ArrayFloat:
                    return "float[]";
                case FieldType.ArrayDouble:
                    return "double[]";
                case FieldType.ArrayString:
                    return "string[]";
                case FieldType.ArrayBoolean:
                    return "bool[]";
                case FieldType.ArrayVector2:
                    return "Vector2[]";
                case FieldType.ArrayVector3:
                    return "Vector3[]";
                case FieldType.Arrayi18N:
                    return "i18nObject[]";
                default:
                    return "";
            }
        }

		#region content to C# value
        /// <summary>
        /// 数据分割
        /// </summary>
        /// <param name="content"></param>
        /// <param name="separator"></param>
        /// <returns></returns>
        private static string[] ContentSeparator(string content, char[] separator)
        {
            return content.Split(separator, StringSplitOptions.RemoveEmptyEntries);
        }
        
        /// <summary>
        /// content 2 bool
        /// </summary>
        /// <param name="content"></param>
        /// <returns></returns>
        public static bool ContentToBooleanValue(string content)
        {
            return !content.Equals("0");
        }

        /// <summary>
        /// content 2 i18nObject
        /// </summary>
        /// <param name="content"></param>
        /// <returns></returns>
        public static I18NObject ContentToI18NObjectValue(string content)
        {
            if (string.IsNullOrEmpty(content)) return null;
            string[] datas = ContentSeparator(content, Separator);
            return new I18NObject(int.Parse(datas[0]), int.Parse(datas[1]));

        }

        /// <summary>
        /// content 2 Vector2
        /// </summary>
        /// <param name="content"></param>
        /// <returns></returns>
        public static Vector2 ContentToVector2TValue(string content)
        {
            if (string.IsNullOrEmpty(content)) return Vector2.zero;
            string[] datas = ContentSeparator(content, Separator);
            return new Vector2(float.Parse(datas[0]), float.Parse(datas[1]));
        }

        /// <summary>
        /// content 2 Vector3
        /// </summary>
        /// <param name="content"></param>
        /// <returns></returns>
        public static Vector3 ContentToVector3TValue(string content)
        {
            if (string.IsNullOrEmpty(content)) return Vector3.zero;
            string[] datas = ContentSeparator(content, Separator);
            return new Vector3(float.Parse(datas[0]), float.Parse(datas[1]), float.Parse(datas[2]));
        }
        
        /// <summary>
        /// context to T[]
        /// </summary>
        /// <param name="content"></param>
        /// <param name="func"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public static T[] ContentToArrayType<T>(string content, Func<string, T> func)
        {
            if (string.IsNullOrEmpty(content)) return null;
            string[] datas = ContentSeparator(content, Separator);
            T[] values = new T[datas.Length];
            for (int i = 0; i < datas.Length; i++)
            {
                values[i] = func(datas[i]);
            }
            return values;
        }
		#endregion
    }
}