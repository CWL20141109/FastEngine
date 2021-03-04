using System.Collections.Generic;
using System.IO;
using ExcelDataReader;
using UnityEngine;

namespace FastEngine.Core.Excel2Table
{
    public class ExcelReader
    {
        private string _mFilePath;
        public ExcelReaderOptions options { get; private set; }
        public List<string> descriptions { get; set; }
        public List<string> fields { get; private set; }
        public List<FieldType> types { get; private set; }
        public List<int> ignoreIndexs { get; private set; }
        public List<ExcelReaderRow> rows { get; private set; }
        public ExcelReader(string filePath, ExcelReaderOptions options)
        {
            _mFilePath = filePath;
            this.options = options;
            descriptions = new List<string>();
            fields = new List<string>();
            types = new List<FieldType>();
            ignoreIndexs = new List<int>();
            rows = new List<ExcelReaderRow>();
        }

        public void Read()
        {
            descriptions.Clear();
            fields.Clear();
            types.Clear();
            ignoreIndexs.Clear();
            rows.Clear();

            FieldType fieldType;
            bool removeIgnore = false;

            using (var stream = File.Open(_mFilePath, FileMode.Open, FileAccess.Read))
            {
                using (var reader = ExcelReaderFactory.CreateReader(stream))
                {
                    var result = reader.AsDataSet();

                    var dataTable = result.Tables[0];
                    var columCount = dataTable.Columns.Count;
                    var rowCount = dataTable.Rows.Count;
                    bool isRun = true;
                    int r = 0;
                    while (isRun)
                    {
                        var row = new ExcelReaderRow();
                        for (int c = 0; c < columCount; c++)
                        {
                            var context = dataTable.Rows[r][c].ToString();

                            if (r == 0)
                            {
                                descriptions.Add(context);
                            }
                            else if (r == 1)
                            {
                                fields.Add(context);
                            }
                            else if (r == 2)
                            {
                                fieldType = TypeUtils.TypeContentToFieldType(context);
                                types.Add(fieldType);
                                if (fieldType == FieldType.Ignore)
                                {
                                    ignoreIndexs.Add(c);
                                }
                                if (string.IsNullOrEmpty(context))
                                {
                                    isRun = false;
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
                                descriptions = RemoveIgnore<string>(descriptions, ignoreIndexs);
                                fields = RemoveIgnore<string>(fields, ignoreIndexs);
                                types = RemoveIgnore<FieldType>(types, ignoreIndexs);
                                removeIgnore = true;
                            }
                            row.descriptions = descriptions;
                            row.fields = fields;
                            row.types = types;
                            row.datas = RemoveIgnore<string>(row.datas, ignoreIndexs);
                            rows.Add(row);
                        }
                        r++;
                        if (r >= rowCount)
                        {
                            isRun = false;
                        }
                    }
                    // for (int r = 0; r < rowCount; r++)
                    // {
                    //    
                    // }

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