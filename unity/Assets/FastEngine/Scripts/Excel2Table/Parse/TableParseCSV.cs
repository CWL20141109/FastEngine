/*
* @Author: cwl
* @Description:
* @Date: 2021-03-04 11:23:37
*/

using System;
using System.Reflection;
using System.Collections.Generic;
using Table;
using UnityEngine;
using Object = System.Object;
namespace FastEngine.Core.Excel2Table
{
	public class TableParseCSV<T> : TableParse<T>
	{
		private readonly char[] lineSeparator = new[] { '\r', '\n' };
		private readonly char[] separator = new[] { '\r', '\n' };

		private Type _type;
		private string[] _lines;
		private FieldInfo[] _fields;
		private TableParse<TestTable> _tableParseImplementation;
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
				_fields[i] = _type.GetField(_strfields[i], BindingFlags.Instance | BindingFlags.Public | BindingFlags.Static | BindingFlags.NonPublic);
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
				array[i - 1] = 
			}
			return array;
		}
		/// <summary>
		/// 解析为字典(key 为 string)
		/// </summary>
		/// <returns></returns>
		public override Dictionary<string, T> ParseStringDictionary()
		{

		}

		/// <summary>
		/// 解析为字典(key 为 int)
		/// </summary>
		/// <returns></returns>
		public override Dictionary<int, T> ParseIntDictionary()
		{

		}
		/// <summary>
		/// 解析为字典(key 为 int2int)
		/// </summary>
		/// <returns></returns>
		public override Dictionary<int, Dictionary<int, T>> ParseInt2IntDictionary()
		{

		}

		private T CreateInstance(string line)
		{
			var obj = Activator.CreateInstance<T>();

			return obj;
		}

		/// <summary>
		/// 设置对象属性值
		/// </summary>
		/// <param name="obj">对象</param>
		/// <param name="field">属性</param>
		/// <param name="value">值</param>
		private void SetValue(Object obj, FieldInfo field, string value)
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
			}else if (fieldType == typeof(I18NObject))
			{
				field.SetValue();
			}

		}
	}

}