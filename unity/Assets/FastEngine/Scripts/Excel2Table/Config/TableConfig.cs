/*
* @Author: cwl
* @Description:
* @Date: 2021-02-26 16:02:18
*/

using System.Collections.Generic;
namespace FastEngine.Core.Excel2Table
{
    public class TableItem
    {
        /// <summary>
        /// 数据表名称
        /// </summary>
        public string TableName { get; set; }
        /// <summary>
        /// 数据格式
        /// </summary>
        public DataFormatOptions DataFormatOptions { get; set; }
    }


    public class TableConfig : ConfigObject
    {
        /// <summary>
        /// 导出格式
        /// </summary>
        public FormatOptions OutFormatOptions;
        /// <summary>
        /// tableModel 命名空间
        /// </summary>
        public string TableModelNamespace { get; set; }
        /// <summary>
        /// 数据表
        /// </summary>
        public Dictionary<string, TableItem> TableDictionary { get; set; }

        protected override void OnInitialize()
        {
            if (TableDictionary == null)
            {
                TableDictionary = new Dictionary<string, TableItem>();
            }
        }
    }
}