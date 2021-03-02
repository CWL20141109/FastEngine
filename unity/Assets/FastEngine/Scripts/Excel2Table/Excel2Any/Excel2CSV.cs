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
			for (int i = 0; i < reader.Fields.Count - 1; i++)
			{
				_mStringBuilder.Append(reader.Fields[i]);
				_mStringBuilder.Append(",");
			}
			_mStringBuilder.Append(reader.Fields[reader.Fields.Count - 1]);
			_mStringBuilder.Append("\r\n");

			// data
			for (int i = 0; i < reader.Rows.Count - 1; i++)
			{
				Dictionary<string, object> data = new Dictionary<string, object>();
				for (int k = 0; k < reader.Fields.Count - 1; k++)
				{
					_mStringBuilder.Append(WrapContext(reader.Rows[i].datas[k], reader.Types[k]));
					_mStringBuilder.Append(",");
				}

				_mStringBuilder.Append(WrapContext(reader.Rows[i].datas[reader.Fields.Count - 1], reader.Types[reader.Fields.Count - 1]));
				_mStringBuilder.Append("\r\n");
			}

			FilePathUtils.FileWriteAllText(reader.Options.DataOutFilePath, _mStringBuilder.ToString());
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
					case FieldType.Double:
					case FieldType.Boolean:
						return content;
					case FieldType.I18N:
						return WrapI18NContext(content);
					case FieldType.Arrayi18N:
						return WrapArrayI18NContext(content);
					default:
						return string.Format("\"{0}\"", content);
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
				var keyField = model.GetField(string.Format("{0}_{1}", datas[0], datas[1]));

				if (modelField != null && keyField != null)
				{
					return string.Format("{0}:{1}", (int)modelField.GetValue(null), (int)keyField.GetValue(null));
				}
				else
				{
					Debug.LogError("[" + _mReader.Options.tableName + "] table not find i18n : " + datas[0] + " : " + datas[1]);
				}
			}
			else
			{
				Debug.LogError("[" + _mReader.Options.tableName + "] table i18n format error!");
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
			result += WrapI18NContext(datas[datas.Length]);
			return string.Format("\"{0}\"", result);
		}
	}
}