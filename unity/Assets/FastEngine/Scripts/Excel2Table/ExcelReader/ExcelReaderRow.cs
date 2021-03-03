using System;
using System.Collections.Generic;

namespace FastEngine.Core.Excel2Table
{
	public class ExcelReaderRow
	{
		/// <summary>
		/// 描述
		/// </summary>
		public List<string> Descriptions;
		/// <summary>
		/// 字段
		/// </summary>
		public List<string> Fields;
		/// <summary>
		/// 类型
		/// </summary>
		public List<FieldType> Types;
		/// <summary>
		/// 数据
		/// </summary>
		public List<string> Datas;

		public ExcelReaderRow()
		{
			Descriptions = new List<string>();
			Fields = new List<string>();
			Types = new List<FieldType>();
			Datas = new List<string>();
		}
	}
}