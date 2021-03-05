/*
* @Author: cwl
* @Description:
* @Date: 2021-03-01 15:12:18
*/

using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using UnityEngine;
namespace FastEngine.Core.Excel2Table
{

	public class Excel2Csv : Excel2Any
	{
		private StringBuilder _mStringBuilder = new StringBuilder();
		private ExcelReader _mReader;
		public Excel2Csv(ExcelReader reader) : base(reader)
		{
			_mReader = reader;

			// fields
			_mStringBuilder.Clear();
			var maxCount = reader.fields.Count - 1;
			for (int i = 0; i < maxCount; i++)
			{
				if (reader.types[i] == FieldType.I18N)
				{
					_mStringBuilder.Append($"_{reader.fields[i]}_I18N");
				}
				else
				{
					_mStringBuilder.Append(reader.fields[i]);
				}
				_mStringBuilder.Append(",");
			}
			if (reader.types[maxCount] == FieldType.I18N)
			{
				_mStringBuilder.Append($"_{reader.fields[maxCount]}_I18N");
			}
			else
			{
				_mStringBuilder.Append(reader.fields[maxCount]);
			}

			_mStringBuilder.Append("\r\n");

			// data
			for (int i = 0; i < reader.rows.Count; i++)
			{
				Dictionary<string, object> data = new Dictionary<string, object>();
				for (int k = 0; k < maxCount; k++)
				{
					_mStringBuilder.Append(WrapContext(reader.rows[i].datas[k], reader.types[k]));
					_mStringBuilder.Append(",");
				}

				_mStringBuilder.Append(WrapContext(reader.rows[i].datas[maxCount], reader.types[maxCount]));
				_mStringBuilder.Append("\r\n");
			}

			FilePathUtils.FileWriteAllText(reader.options.dataOutFilePath, _mStringBuilder.ToString());
		}

		private string WrapContext(string content, FieldType type)
		{
			if (string.IsNullOrEmpty(content))
			{
				switch (type)
				{
					case FieldType.Byte:
					case FieldType.Int:
					case FieldType.Long:
					case FieldType.Float:
					case FieldType.Double:
					case FieldType.Boolean:
						return "0";
					default:
						return "\"\"";
				}
			}
			else
			{
				switch (type)
				{
					case FieldType.Byte:
					case FieldType.Int:
					case FieldType.Long:
					case FieldType.Float:
					case FieldType.Double:
					case FieldType.Boolean:
						return content;
					case FieldType.I18N:
						return WrapI18NContext(content);
					case FieldType.ArrayI18N:
						return WrapArrayI18NContext(content);
					default:
						return $"\"{content}\"";
				}
			}
		}

		private string WrapI18NContext(string content)
		{
			char[] separator = new[] { ':' };
			string[] datas = content.Split(separator, StringSplitOptions.RemoveEmptyEntries);
			if (datas.Length == 2)
			{
				var model = typeof(LanguageModel);
				var modelField = model.GetField(datas[0]);

				var key = typeof(LanaguageKey);
				var keyField = key.GetField($"{datas[0]}_{datas[1]}");

				if (modelField != null && keyField != null)
				{
					return $"{(int)modelField.GetValue(null)}:{(int)keyField.GetValue(null)}";
				}
				else
				{
					Debug.LogError($"[ {_mReader.options.tableName}] table not find i18n : {datas[0]}  : {datas[1]}");
				}
			}
			else
			{
				Debug.LogError($"[{_mReader.options.tableName}] table i18n format error!");
			}
			return "0:0";
		}

		private string WrapArrayI18NContext(string content)
		{
			char[] separator = new char[] { ',' };
			string[] datas = content.Split(separator, StringSplitOptions.RemoveEmptyEntries);
			string result = "";
			for (int i = 0; i < datas.Length - 1; i++)
			{
				result += WrapI18NContext(datas[i]) + ",";
			}
			result += WrapI18NContext(datas[datas.Length - 1]);
			return $"\"{result}\"";
		}
	}
}