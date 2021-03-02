/*
* @Author: cwl
* @Description: excel转json
* @Date: 2021-03-02 11:55:26
*/

using System.Collections.Generic;
using Newtonsoft.Json;
namespace FastEngine.Core.Excel2Table
{
	public class Excel2Json : Excel2Any
	{
		public Excel2Json(ExcelReader reader) : base(reader)
		{
			List<Dictionary<string, object>> dataList = new List<Dictionary<string, object>>();
			for (int i = 0; i < reader.Rows.Count; i++)
			{
				Dictionary<string, object> data = new Dictionary<string, object>();
				for (int k = 0; k < reader.Fields.Count; k++)
				{
					data.Add(reader.Fields[k], reader.Rows[i].datas[k]);
				}
				dataList.Add(data);
			}
			FilePathUtils.FileWriteAllText(reader.Options.DataOutFilePath, JsonConvert.SerializeObject(dataList, Newtonsoft.Json.Formatting.Indented));
		}
	}
}