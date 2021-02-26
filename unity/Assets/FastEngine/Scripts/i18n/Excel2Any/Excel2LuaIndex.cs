using System.Text;
using UnityEngine;

namespace FastEngine.Core.I18n
{
	public class Excel2LuaIndex
	{
		public StringBuilder m_bilder = new StringBuilder();

		public Excel2LuaIndex(ExcelReader reader)
		{
			m_bilder.Clear();

			// model
			m_bilder.AppendLine("language_model = {");
			for (int i = 0; i < reader.sheets.Length; i++)
			{
				m_bilder.AppendLine(string.Format("\t{0} = {1},", reader.sheets[i].name, i));
			}
			m_bilder.AppendLine("}");
			

			// key
			m_bilder.AppendLine("language_key = {");
			for (int i = 0; i < reader.sheets.Length; i++)
			{
				m_bilder.AppendLine(reader.sheets[i].ToLuaKeyString());
			}
			m_bilder.AppendLine("}");

			FilePathUtils.FileWriteAllText(FilePathUtils.Combine(Application.dataPath, "LuaScripts/language.lua"), m_bilder.ToString());
		}
	}
}