/*
* @Author: cwl
* @Description:
* @Date: 2021-03-04 11:23:37
*/

using System;
using System.Reflection;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

namespace FastEngine.Core.Excel2Table
{
	public class TableParseCSV<T> : TableParse<T>
	{
		private readonly char[] lineSeparator = new[] { '\r', '\n' };
		private readonly char[] separator = new[] { ',' };

		private Type _type;
		private string[] _lines;
		private FieldInfo[] _fields;
		private TableParse<T> _tableParseImplementation;
		public TableParseCSV(string tableName) : base(tableName, FormatOptions.CSV)
		{
			LoadAsset();
			BuildData();
		}

		/// <summary>
		/// 构建数据
		/// </summary>
		/// <typeparam name="T"></typeparam>
		private void BuildData()
		{
			_type = typeof(T);
			// read lines
			_lines = content.Split(lineSeparator, StringSplitOptions.RemoveEmptyEntries);

			//create field info
			var _strfields = _lines[0].Split(separator, StringSplitOptions.RemoveEmptyEntries);
			_fields = new FieldInfo[_strfields.Length];
			for (int i = 0; i < _strfields.Length; i++)
			{
				_fields[i] = _type.GetField(_strfields[i], BindingFlags.Instance | BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic);
			}
		}


		/// <summary>
		/// 解析为数组
		/// </summary>
		/// <returns></returns>
		public override T[] ParseArray()
		{
			T[] array = new T[_lines.Length - 1];
			for (int i = 1; i < _lines.Length; i++)
			{
				array[i - 1] = CreateInstance(ParseLine(_lines[i]));
			}
			return array;
		}
		/// <summary>
		/// 解析为字典(key 为 string)
		/// </summary>
		/// <returns></returns>
		public override Dictionary<string, T> ParseStringDictionary()
		{
			Dictionary<string, T> dictionary = new Dictionary<string, T>();
			string[] lc = null;
			for (int i = 1; i < _lines.Length; i++)
			{
				lc = ParseLine(_lines[i]);
				dictionary.Add(lc[0], CreateInstance(lc));
			}
			return dictionary;
		}

		/// <summary>
		/// 解析为字典(key 为 int)
		/// </summary>
		/// <returns></returns>
		public override Dictionary<int, T> ParseIntDictionary()
		{
			Dictionary<int, T> dictionary = new Dictionary<int, T>();
			string[] lc = null;
			for (int i = 1; i < _lines.Length; i++)
			{
				lc = ParseLine(_lines[i]);
				dictionary.Add(int.Parse(lc[0]), CreateInstance(lc));
			}
			return dictionary;
		}
		/// <summary>
		/// 解析为字典(key 为 int2int)
		/// </summary>
		/// <returns></returns>
		public override Dictionary<int, Dictionary<int, T>> ParseInt2IntDictionary()
		{
			Dictionary<int, Dictionary<int, T>> dictionary = new Dictionary<int, Dictionary<int, T>>();
			string[] lc = null;
			int key1 = -1;
			int key2 = -1;
			for (int i = 1; i < _lines.Length; i++)
			{
				lc = ParseLine(_lines[i]);
				key1 = int.Parse(lc[0]);
				key2 = int.Parse(lc[1]);
				lc = ParseLine(_lines[i]);
				if (!dictionary.ContainsKey(key1))
				{
					dictionary.Add(key1, new Dictionary<int, T>());
				}
				dictionary[key1].Add(key2, CreateInstance(lc));
			}
			return dictionary;
		}

		/// <summary>
		/// 创建对象
		/// </summary>
		/// <param name="line"></param>
		/// <returns></returns>
		private T CreateInstance(string[] line)
		{
			var obj = Activator.CreateInstance<T>();
			for (int i = 0; i < _fields.Length; i++)
			{
				SetValue(obj, _fields[i], line[i]);
			}
			return obj;
		}

		/// <summary>
		/// 设置对象属性值
		/// </summary>
		/// <param name="obj">对象</param>
		/// <param name="field">属性</param>
		/// <param name="value">值</param>
		private void SetValue(object obj, FieldInfo field, string value)
		{
			var fieldType = field.FieldType;
			if (fieldType == typeof(byte))
			{
				field.SetValue(obj, byte.Parse(value));
			}
			else if (fieldType == typeof(int))
			{
				field.SetValue(obj, int.Parse(value));
			}
			else if (fieldType == typeof(long))
			{
				field.SetValue(obj, long.Parse(value));
			}
			else if (fieldType == typeof(float))
			{
				field.SetValue(obj, float.Parse(value));
			}
			else if (fieldType == typeof(double))
			{
				field.SetValue(obj, double.Parse(value));
			}
			else if (fieldType == typeof(string))
			{
				field.SetValue(obj, value);
			}
			else if (fieldType == typeof(bool))
			{
				field.SetValue(obj, TypeUtils.ContentToBooleanValue(value));
			}
			else if (fieldType == typeof(Vector2))
			{
				field.SetValue(obj, TypeUtils.ContentToVector2TValue(value));
			}
			else if (fieldType == typeof(Vector3))
			{
				field.SetValue(obj, TypeUtils.ContentToVector3TValue(value));
			}
			else if (fieldType == typeof(I18NObject))
			{
				field.SetValue(obj, TypeUtils.ContentToI18NObjectValue(value));
			}
			else if (fieldType == typeof(byte[]))
			{
				field.SetValue(obj, TypeUtils.ContentToArrayByteValue(value));
			}
			else if (fieldType == typeof(int[]))
			{
				field.SetValue(obj, TypeUtils.ContentToArrayIntValue(value));
			}
			else if (fieldType == typeof(long[]))
			{
				field.SetValue(obj, TypeUtils.ContentToArrayLongValue(value));
			}
			else if (fieldType == typeof(float[]))
			{
				field.SetValue(obj, TypeUtils.ContentToArrayFloatValue(value));
			}
			else if (fieldType == typeof(double[]))
			{
				field.SetValue(obj, TypeUtils.ContentToArrayDoubleValue(value));
			}
			else if (fieldType == typeof(string[]))
			{
				field.SetValue(obj, TypeUtils.ContentToArrayStringValue(value));
			}
			else if (fieldType == typeof(bool[]))
			{
				field.SetValue(obj, TypeUtils.ContentToArrayBooleanValue(value));
			}
			else if (fieldType == typeof(Vector2[]))
			{
				field.SetValue(obj, TypeUtils.ContentToArrayVector2Value(value));
			}
			else if (fieldType == typeof(Vector3[]))
			{
				field.SetValue(obj, TypeUtils.ContentToArrayVector3Value(value));
			}
			else if (fieldType == typeof(I18NObject[]))
			{
				field.SetValue(obj, TypeUtils.ContentToArrayI18NObjectValue(value));
			}
		}

		/// <summary>
		/// 解析内容
		/// </summary>
		/// <param name="text"></param>
		/// <returns></returns>
		public string[] ParseLine(string text)
		{
			var separator = ',';
			var line = new List<string>();
			StringBuilder token = new StringBuilder();
			bool quotes = false;
			for (int i = 0; i < text.Length; i++)
			{
				if (quotes)
				{
					if ((text[i] == '\\' && i + 1 < text.Length && text[i + 1] == '\"') || (text[i] == '\"' && i + i < text.Length && text[i + i] == '\"'))
					{
						token.Append('\"');
						i++;
					}
					else if (text[i] == '\\' && i + 1 < text.Length && text[i + 1] == 'n')
					{
						token.Append('\n');
						i++;
					}
					else if (text[i] == '\"')
					{

						if (i + 1 < text.Length && text[i + 1] == separator || i == text.Length - 1)
						{
							line.Add(token.ToString());
							token.Clear();
							quotes = false;
							i++;
						}
						else
						{
							token.Append(text[i]);
						}
					}
					else
					{
						token.Append(text[i]);
					}
				}
				else if (text[i] == '\r' || text[i] == '\n')
				{
					if (token.Length > 0)
					{
						line.Add(token.ToString());
						token.Clear();
					}
					if (line.Count > 0)
					{
						return line.ToArray();
					}
				}
				else if (text[i] == separator)
				{
					line.Add(token.ToString());
					token.Clear();
				}
				else if (text[i] == '\"')
				{
					quotes = true;
				}
				else
				{
					token.Append(text[i]);
				}
			}
			if (token.Length > 0)
			{
				line.Add((token.ToString()));
			}
			return line.ToArray();
		}

	}
}