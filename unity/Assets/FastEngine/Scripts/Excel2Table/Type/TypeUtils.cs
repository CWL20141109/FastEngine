/*
* @Author: 
* @Description:
* @Date: 2021-02-28 16:51:18
*/

using System;
namespace FastEngine.Core.Excel2Table
{
    public class TypeUtils
    {

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
                case FieldType.i18n:
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
                case FieldType.Arrayi18n:
                    return "i18nObject[]";
                default:
                    return "";
            }
        }

    }
}