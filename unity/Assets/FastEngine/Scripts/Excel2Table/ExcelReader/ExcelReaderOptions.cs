using System;

namespace FastEngine.Core.Excel2Table
{
    /// <summary>
    /// 格式
    /// </summary>
    public enum FormatOptions
    {
        Csv,
        Json,
        Lua,
    }

    /// <summary>
    /// 数据格式
    /// </summary>
    public enum DataFormatOptions
    {
        Array,
        IntDictionary,
        StringDictionary,
        Int2IntDictionary,
    }

    public class ExcelReaderOptions
    {
        /// <summary>
        /// 导出格式
        /// </summary>
        public FormatOptions OutFormatOptions;
        /// <summary>
        /// 数据格式
        /// </summary>
        public DataFormatOptions DataFormatOptions;
        /// <summary>
        /// 表名字
        /// </summary>
        public string TableName;
        /// <summary>
        /// tableModel 的命名空间
        /// </summary>
        public string TableModelNamespace;
        /// <summary>
        /// 数据文件输出目录
        /// </summary>
        public string DataOutDirectory;
        /// <summary>
        /// tableModel 文件输出目录
        /// </summary>
        public string TableModelOutDirectory;
        /// <summary>
        /// lua 文件输出目录
        /// </summary>
        public string LuaOutDirectory;

        /// <summary>
        /// 数据文件输出路径
        /// </summary>
        public string DataOutFilePath
        {
            get
            {
                if (OutFormatOptions == FormatOptions.Csv) return FilePathUtils.Combine(DataOutDirectory, TableName + ".csv");
                else return FilePathUtils.Combine(DataOutDirectory, TableName + ".csv");
            }
        }

        /// <summary>
        /// table model  文件输出路径
        /// </summary>
        public string TableModelOutFilePath { get { return FilePathUtils.Combine(TableModelOutDirectory, TableName + "Table.cs"); } }

        /// <summary>
        /// lua 文件输出路径
        /// </summary>
        public string LuaOutFilePath { get { return FilePathUtils.Combine(LuaOutDirectory, TableName + ".lua"); } }

    }
}

