using System.Text;

namespace FastEngine.Core.I18n
{
	public class ExcelSheet
	{
		public string name;
		public ExcelColumn[] columns;

		public string ToLuaKeyString()
		{
			StringBuilder builder = new StringBuilder();
			for (int i = 1; i < columns.Length; i++)
			{
				builder.AppendLine(string.Format("\t{0}_{1} = {2},", name, columns[i].key, i - 1));
			}
			return builder.ToString().TrimEnd('\r', '\n');
		}

		public string ToCSharpKeyString()
		{
			StringBuilder builder = new StringBuilder();
			for (int i = 1; i < columns.Length; i++)
			{
				builder.AppendLine(string.Format("\tpublic static int {0}_{1} = {2};", name, columns[i].key, i - 1));
			}
			return builder.ToString().TrimEnd('\r', '\n');
		}

		public string ToValueString(int index)
		{
			StringBuilder builder = new StringBuilder();

			for (int i = 1; i < columns.Length; i++)
			{
				builder.AppendLine(columns[i].values[index].TrimEnd('\r', '\n'));
			}
			return builder.ToString().TrimEnd('\r', '\n');
		}

		public override string ToString()
		{
			return "sheet name :" + name + " column count: " + columns.Length;
		}
	}
}