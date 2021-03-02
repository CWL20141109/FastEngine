using System.Text;

namespace FastEngine.Core.I18n
{
	public class Excel2Index
	{
		private StringBuilder _mBuilder = new StringBuilder();

		public Excel2Index(ExcelReader reader)
		{
			_mBuilder.Clear();

			// model
			_mBuilder.AppendLine("public static class LanguageModel");
			_mBuilder.AppendLine("{");

			for (int i = 0; i < reader.Sheets.Length; i++)
			{
				_mBuilder.AppendLine(string.Format("\tpublic static int {0} = {1};", reader.Sheets[i].name, i));
			}
			_mBuilder.AppendLine("}");

			_mBuilder.AppendLine("public static class LanaguageKey");
			_mBuilder.AppendLine("{");

			for (int i = 0; i < reader.Sheets.Length; i++)
			{
				_mBuilder.AppendLine(reader.Sheets[i].ToCSharpKeyString());
			}
			_mBuilder.AppendLine("}");
			FilePathUtils.FileWriteAllText(AppUtils.I18NIndexDirectory() + "/i18nIndex.cs", _mBuilder.ToString());
		}
	}
}