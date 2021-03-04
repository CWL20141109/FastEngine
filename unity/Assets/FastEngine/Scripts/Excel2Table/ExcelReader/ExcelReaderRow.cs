using System;
using System.Collections.Generic;

namespace FastEngine.Core.Excel2Table
{
	public class ExcelReaderRow
	{
		/// <summary>
		/// 描述
		/// </summary>
		public List<string> descriptions;
		/// <summary>
		/// 字段
		/// </summary>
		public List<string> fields;
		/// <summary>
		/// 类型
		/// </summary>
		public List<FieldType> types;
		/// <summary>
		/// 数据
		/// </summary>
		public List<string> datas;

		public ExcelReaderRow()
		{
			descriptions = new List<string>();
			fields = new List<string>();
			types = new List<FieldType>();
			datas = new List<string>();
		}
	}
}