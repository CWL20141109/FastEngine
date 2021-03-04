using System;

namespace FastEngine.Core.Excel2Table
{
    /// <summary>
    /// 格式
    /// </summary>
    public enum FormatOptions
    {
        CSV,
        JSON,
        LUA,
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
        public FormatOptions outFormatOptions;
        /// <summary>
        /// 数据格式
        /// </summary>
        public DataFormatOptions dataFormatOptions;
        /// <summary>
        /// 表名字
        /// </summary>
        public string tableName;
        /// <summary>
        /// tableModel 的命名空间
        /// </summary>
        public string tableModelNamespace;
        /// <summary>
        /// 数据文件输出目录
        /// </summary>
        public string dataOutDirectory;
        /// <summary>
        /// tableModel 文件输出目录
        /// </summary>
        public string tableModelOutDirectory;
        /// <summary>
        /// lua 文件输出目录
        /// </summary>
        public string luaOutDirectory;

        /// <summary>
        /// 数据文件输出路径
        /// </summary>
        public string dataOutFilePath
        {
            get
            {
                if (outFormatOptions == FormatOptions.CSV) return FilePathUtils.Combine(dataOutDirectory, tableName + ".csv");
                else return FilePathUtils.Combine(dataOutDirectory, tableName + ".csv");
            }
        }

        /// <summary>
        /// table model  文件输出路径
        /// </summary>
        public string tableModelOutFilePath { get { return FilePathUtils.Combine(tableModelOutDirectory, tableName + "Table.cs"); } }

        /// <summary>
        /// lua 文件输出路径
        /// </summary>
        public string luaOutFilePath { get { return FilePathUtils.Combine(luaOutDirectory, tableName + ".lua"); } }

    }
}

