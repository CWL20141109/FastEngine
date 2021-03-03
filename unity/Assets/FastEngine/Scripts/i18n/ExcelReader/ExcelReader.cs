using System.IO;
using ExcelDataReader;

namespace FastEngine.Core.I18n
{
	public class ExcelReader
	{
		public ExcelReaderOptions Options { get; private set; }
		public ExcelSheet[] Sheets { get; private set; }

		public ExcelReader(ExcelReaderOptions options)
		{
			this.Options = options;
		}

		public void Read()
		{
			using (var stream = File.Open(AppUtils.I18NExcelFilePath(), FileMode.Open, FileAccess.Read))
			{
				using (var reader = ExcelReaderFactory.CreateReader(stream))
				{
					Sheets = new ExcelSheet[reader.ResultsCount];

					var result = reader.AsDataSet();
					for (int i = 0; i < reader.ResultsCount; i++)
					{
						var dataTable = result.Tables[i];
						var columnCount = dataTable.Columns.Count;
						var rowCount = dataTable.Rows.Count;

						var sheet = new ExcelSheet();
						sheet.Name = dataTable.TableName;
						sheet.Columns = new ExcelColumn[rowCount];

						sheet.Columns[0] = new ExcelColumn();
						for (int r = 1; r < rowCount; r++)
						{
							var excelColumn = new ExcelColumn();
							excelColumn.Values = new string[columnCount];
							for (int c = 0; c < columnCount; c++)
							{
								var context = dataTable.Rows[r][c].ToString();
								if (c == 0)
								{
									excelColumn.Key = context;
								}
								else
								{
									excelColumn.Values[c] = context;
								}
							}
							sheet.Columns[r] = excelColumn;
						}
						Sheets[i] = sheet;
					}
				}
			}
		}
	}
}