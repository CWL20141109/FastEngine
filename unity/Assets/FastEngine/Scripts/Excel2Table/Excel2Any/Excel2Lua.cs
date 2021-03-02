/*
* @Author: cwl
* @Description: excel转lua
* @Date: 2021-03-02 14:30:17
*/

using System.Text;
using UnityEngine;
namespace FastEngine.Core.Excel2Table
{
    public class Excel2Lua : Excel2Any
    {
        private static string _template = "@-- FastEngine\r\n-- excel2table auto generate\r\n-- $descriptions$\r\nlocal datas ={$line$}\r\nreturn datas";

        private StringBuilder _mStringBuilder = new StringBuilder();
        private StringBuilder _mWrapStringBuilder = new StringBuilder();
        public Excel2Lua(ExcelReader reader) : base(reader)
        {
            // descriptions
            for (int i = 0; i < reader.Descriptions.Count; i++)
            {
                _mStringBuilder.Append(reader.Descriptions[i] + ",");
            }
            _template = _template.Replace("$descriptions$", _mStringBuilder.ToString());

            // data
            _mStringBuilder.Clear();
            for (int i = 0; i < reader.Rows.Count; i++)
            {
                _mStringBuilder.Append("\t[" + i + "] = {");
                for (int k = 0; k < reader.Fields.Count - 1; k++)
                {
                    _mStringBuilder.Append(string.Format("{0} = {1},", reader.Fields[k], WrapContext(reader.Rows[i].datas[k], reader.Types[k])));
                }
            }
        }


        string WrapContext(string context, FieldType type)
        {
            switch (type)
            {
                case FieldType.Boolean:
                    return TypeUtils.ContentToBooleanValue(context) ? "true" : "false";
                case FieldType.String:
                    return string.Format("\"{0}\"", context);
                case FieldType.Vector2:
                    Vector2 v2 = TypeUtils.ContentToVector2TValue(context);
                    return string.Format("Vector2.New({0}, {1})", v2.x, v2.y);
                case FieldType.Vector3:
                    Vector3 v3 = TypeUtils.ContentToVector3TValue(context);
                    return string.Format("Vector2.New({0}, {1}, {2})", v3.x, v3.y, v3.z);
                case FieldType.ArrayByte:
                case FieldType.ArrayInt:
                case FieldType.ArrayDouble:
                case FieldType.ArrayFloat:
                case FieldType.ArrayLong:
                    return "{" + context + "}";
                case FieldType.ArrayBoolean:
                    _mWrapStringBuilder.Clear();
                    _mWrapStringBuilder.Append("{");
                    bool[] bools = TypeUtils.ContentToArrayType<bool>(context, TypeUtils.ContentToBooleanValue);
                    for (int i = 0; i < bools.Length - 1; i++)
                    {
                        _mWrapStringBuilder.Append(bools[i] ? "true, " : "false, ");
                    }
                    _mStringBuilder.Append(bools[bools.Length] ? "true }" : "false }");
                    return _mStringBuilder.ToString();
                default:
                    return context;
            }
        }
    }


}