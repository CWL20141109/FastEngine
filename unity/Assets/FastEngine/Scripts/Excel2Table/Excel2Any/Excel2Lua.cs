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
					_mStringBuilder.Append($"{reader.Fields[k]} = {WrapContext(reader.Rows[i].Datas[k], reader.Types[k])}, ");
				}
				_mStringBuilder.Append($"{reader.Fields[reader.Fields.Count - 1]} = {WrapContext(reader.Rows[i].Datas[reader.Fields.Count - 1], reader.Types[reader.Fields.Count - 1])}");
				_mStringBuilder.Append("},\r\n");
			}
			_mStringBuilder.Append("}\r\n");
			_template = _template.Replace("$line", _mStringBuilder.ToString());
			FilePathUtils.FileWriteAllText(reader.Options.LuaOutFilePath, _template);
		}


		string WrapContext(string context, FieldType type)
		{

			switch (type)
			{
				case FieldType.Boolean:
					return TypeUtils.ContentToBooleanValue(context) ? "true" : "false";
				case FieldType.String:
					return $"\"{context}\"";
				case FieldType.Vector2:
					{
						Vector2 v2 = TypeUtils.ContentToVector2TValue(context);
						return $"Vector2.New({v2.x},{v2.y})";
					}
				case FieldType.Vector3:
					{
						Vector3 v3 = TypeUtils.ContentToVector3TValue(context);
						return ($"Vector3.New({v3.x},{v3.y},{v3.z})");
					}
				case FieldType.ArrayByte:
				case FieldType.ArrayInt:
				case FieldType.ArrayDouble:
				case FieldType.ArrayFloat:
				case FieldType.ArrayLong:
					return $"{{{context}}}";
				case FieldType.ArrayBoolean:
					{
						_mWrapStringBuilder.Clear();
						_mWrapStringBuilder.Append("{");
						bool[] values = TypeUtils.ContentToArrayBooleanValue(context);
						int max = values.Length - 1;
						for (int i = 0; i < max; i++)
						{
							_mWrapStringBuilder.Append(values[i] ? "true, " : "false, ");
						}
						_mStringBuilder.Append(values[max] ? "true }" : "false }");
						return _mWrapStringBuilder.ToString();
					}

				case FieldType.ArrayString:
					{
						string[] values = TypeUtils.ContentToArrayString2Value(context);
						int max = values.Length - 1;
						for (int i = 0; i < max; i++)
						{
							_mWrapStringBuilder.Append($"\"{values[i]}\",");
						}
						_mWrapStringBuilder.Append($"\"{values[max]}\" }}");
						return _mWrapStringBuilder.ToString();
					}
				case FieldType.ArrayVector2:
					{
						_mWrapStringBuilder.Clear();
						_mStringBuilder.Append("{");
						Vector2[] values = TypeUtils.ContentToArrayVector2Value(context);
						int max = values.Length - 1;
						for (int i = 0; i < max; i++)
						{
							_mStringBuilder.Append($"Vector2.New({values[i].x},{values[i].y}), ");
						}
						_mStringBuilder.Append($"Vector2.New({values[max].x},{values[max].y}) }} ");
						return _mWrapStringBuilder.ToString();
					}
				case FieldType.ArrayVector3:
					{
						_mWrapStringBuilder.Clear();
						_mStringBuilder.Append("{");
						Vector3[] values = TypeUtils.ContentToArrayVector3Value(context);
						int max = values.Length - 1;
						for (int i = 0; i < max; i++)
						{
							_mStringBuilder.Append($"Vector3.New({values[i].x}, {values[i].y}, {values[i].z}), ");
						}
						_mStringBuilder.Append($"Vector2.New({values[max].x}, {values[max].y} ,{values[max].z}) }} ");
						return _mWrapStringBuilder.ToString();
					}
				default:
					return context;
			}

		}
	}


}