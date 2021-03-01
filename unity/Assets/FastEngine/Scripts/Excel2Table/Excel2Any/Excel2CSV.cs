/*
* @Author: cwl
* @Description:
* @Date: 2021-03-01 15:12:18
*/

using System;
using System.Data;
using System.Text;
using UnityEngine;
namespace FastEngine.Core.Excel2Table
{

	public class Excel2CSV : Excel2Any
	{
		private StringBuilder m_stringBuilder = new StringBuilder();
		private ExcelReader m_reader;
		public Excel2CSV(ExcelReader reader) : base(reader)
		{
			m_reader = reader;

			m_stringBuilder.Clear();
			for (int i = 0; i < reader.fields.Count - 1; i++)
			{
				m_stringBuilder.Append();
			}
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
					case FieldType.i18n:
						return WrapI18nContext(content);
					case FieldType.Arrayi18n:
						return;
					default:
						return string.Format("\"{0}\"", content);
				}
			}
		}

		private string WrapI18nContext(string content)
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
					Debug.LogError("["+m_reader.options.tableName+"] table not find i18n : "+datas[0]+" : "+datas[1]);
				}
			}
			else
			{
				Debug.LogError("["+m_reader.options.tableName+"] table i18n format error!");
			}
			return "0:0";
		}
	}
}