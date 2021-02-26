using System.Text;

namespace FastEngine.Core.I18n
{
	public class Excel2Index
	{
		private StringBuilder m_builder = new StringBuilder();

		public Excel2Index(ExcelReader reader)
		{
			m_builder.Clear();

			// model
			m_builder.AppendLine("public static class LanguageModel");
			m_builder.AppendLine("{");

			for (int i = 0; i < reader.sheets.Length; i++)
			{
				m_builder.AppendLine(string.Format("\tpublic static int {0} = {1};", reader.sheets[i].name, i));
			}
			m_builder.AppendLine("}");

			m_builder.AppendLine("public static class LanaguageKey");
			m_builder.AppendLine("{");

			for (int i = 0; i < reader.sheets.Length; i++)
			{
				m_builder.AppendLine(reader.sheets[i].ToCSharpKeyString());
			}
			m_builder.AppendLine("}");
			FilePathUtils.FileWriteAllText(AppUtils.i18nIndexDirectory() + "/i18nIndex.cs", m_builder.ToString());
		}
	}
}