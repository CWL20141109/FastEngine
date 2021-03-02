using System.Collections.Generic;
using System.IO;
using ExcelDataReader;

namespace FastEngine.Core.Excel2Table
{
    public class ExcelReader
    {
        private string _mFilePath;
        public ExcelReaderOptions Options { get; private set; }
        public List<string> Descriptions { get; set; }
        public List<string> Fields { get; private set; }
        public List<FieldType> Types { get; private set; }
        public List<int> IgnoreIndexs { get; private set; }
        public List<ExcelReaderRow> Rows { get; private set; }
        public ExcelReader(string filePath, ExcelReaderOptions options)
        {
            _mFilePath = filePath;
            this.Options = options;
            Descriptions = new List<string>();
            Fields = new List<string>();
            Types = new List<FieldType>();
            IgnoreIndexs = new List<int>();
            Rows = new List<ExcelReaderRow>();
        }

        public void Read()
        {
            Descriptions.Clear();
            Fields.Clear();
            Types.Clear();
            IgnoreIndexs.Clear();
            Rows.Clear();

            FieldType fieldType;
            bool removeIgnore = false;

            using (var stream = File.Open(_mFilePath, FileMode.Open, FileAccess.Read))
            {
                using (var reader = ExcelReaderFactory.CreateReader(stream))
                {
                    var result = reader.AsDataSet();
                    for (int i = 0; i < reader.ResultsCount; i++)
                    {
                        var dataTable = result.Tables[i];
                        var columCount = dataTable.Columns.Count;
                        var rowCount = dataTable.Rows.Count;

                        for (int r = 0; r < rowCount; r++)
                        {
                            var row = new ExcelReaderRow();
                            for (int c = 0; c < columCount; c++)
                            {
                                var context = dataTable.Rows[r][c].ToString();
                                if (r == 0)
                                {
                                    Descriptions.Add(context);
                                }
                                else if (r == 1)
                                {
                                    Fields.Add(context);
                                }
                                else if (r == 2)
                                {
                                    fieldType = TypeUtils.TypeContentToFieldType(context);
                                    Types.Add(fieldType);
                                    if (fieldType == FieldType.Ignore)
                                    {
                                        IgnoreIndexs.Add(c);
                                    }
                                }
                                else
                                {
                                    row.datas.Add(context);
                                }
                            }

                            if (r > 2)
                            {
                                if (!removeIgnore)
                                {
                                    Descriptions = RemoveIgnore<string>(Descriptions, IgnoreIndexs);
                                    Fields = RemoveIgnore<string>(Fields, IgnoreIndexs);
                                    Types = RemoveIgnore<FieldType>(Types, IgnoreIndexs);
                                    removeIgnore = true;
                                }
                                row.descriptions = Descriptions;
                                row.fields = Fields;
                                row.types = Types;
                                row.datas = RemoveIgnore<string>(row.datas, IgnoreIndexs);
                                Rows.Add(row);
                            }
                        }

                    }
                }
            }
        }

        /// <summary>
        /// 溢出忽略项
        /// </summary>
        /// <param name="sources"></param>
        /// <param name="indexs"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public List<T> RemoveIgnore<T>(List<T> sources, List<int> indexs)
        {
            int count = indexs.Count;
            int index = 0;
            int step = 0;
            while (count > 0)
            {
                index = indexs[step] - step;

                sources.RemoveAt(index);
                count--;
                step++;
            }
            return sources;
        }
    }
}