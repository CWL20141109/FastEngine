using System.IO;
using ExcelDataReader;

namespace FastEngine.Core.I18n
{
	public class ExcelReader
	{
		public ExcelReaderOptions options { get; private set; }
		public ExcelSheet[] sheets { get; private set; }

		public ExcelReader(ExcelReaderOptions options)
		{
			this.options = options;
		}

		public void Read()
		{
			using (var stream = File.Open(AppUtils.i18nExcelFilePath(), FileMode.Open, FileAccess.Read))
			{
				using (var reader = ExcelReaderFactory.CreateReader(stream))
				{
					sheets = new ExcelSheet[reader.ResultsCount];

					var result = reader.AsDataSet();
					for (int i = 0; i < reader.ResultsCount; i++)
					{
						var dataTable = result.Tables[i];
						var columnCount = dataTable.Columns.Count;
						var rowCount = dataTable.Rows.Count;

						var sheet = new ExcelSheet();
						sheet.name = dataTable.TableName;
						sheet.columns = new ExcelColumn[rowCount];

						sheet.columns[0] = new ExcelColumn();
						for (int r = 1; r < rowCount; r++)
						{
							var excelColumn = new ExcelColumn();
							excelColumn.values = new string[columnCount];
							for (int c = 0; c < columnCount; c++)
							{
								var context = dataTable.Rows[r][c].ToString();
								if (c == 0)
								{
									excelColumn.key = context;
								}
								else
								{
									excelColumn.values[c] = context;
								}
							}
							sheet.columns[r] = excelColumn;
						}
						sheets[i] = sheet;
					}
				}
			}
		}
	}
}