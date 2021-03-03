using System.Text;

namespace FastEngine.Core.I18n
{
	public class ExcelSheet
	{
		public string Name;
		public ExcelColumn[] Columns;

		public string ToLuaKeyString()
		{
			StringBuilder builder = new StringBuilder();
			for (int i = 1; i < Columns.Length; i++)
			{
				builder.AppendLine(string.Format("\t{0}_{1} = {2},", Name, Columns[i].Key, i - 1));
			}
			return builder.ToString().TrimEnd('\r', '\n');
		}

		public string ToCSharpKeyString()
		{
			StringBuilder builder = new StringBuilder();
			for (int i = 1; i < Columns.Length; i++)
			{
				builder.AppendLine(string.Format("\tpublic static int {0}_{1} = {2};", Name, Columns[i].Key, i - 1));
			}
			return builder.ToString().TrimEnd('\r', '\n');
		}

		public string ToValueString(int index)
		{
			StringBuilder builder = new StringBuilder();

			for (int i = 1; i < Columns.Length; i++)
			{
				builder.AppendLine(Columns[i].Values[index].TrimEnd('\r', '\n') + "\n");
			}
			return builder.ToString().TrimEnd('\r', '\n');
		}

		public override string ToString()
		{
			return "sheet name :" + Name + " column count: " + Columns.Length;
		}
	}
}