using System.Collections.Generic;

namespace FastEngine.Core.Excel2Table
{
    public class ExcelReader
    {
        private string m_filePath;
        public ExcelReaderOptions options { get; private set; }
        public List<string> descriptions { get; private set; }
        public List<string> field { get; private set; }
        public List<FieldType> types { get; private set; }
        public List<int> ignoreIndexs { get; private set; }
        public List<ExcelReaderRow> rows { get; private set; }

        public ExcelReader(string filePath, ExcelReaderOptions options)
        {
            m_filePath = filePath;
            this.options = options;
            descriptions = new List<string>();
            field = new List<string>();
            types = new List<FieldType>();
            ignoreIndexs = new List<int>();
            rows = new List<ExcelReaderRow>();
        }
    }
}