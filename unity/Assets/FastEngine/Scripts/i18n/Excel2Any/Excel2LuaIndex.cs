using System.Text;
using UnityEngine;

namespace FastEngine.Core.I18n
{
	public class Excel2LuaIndex
	{
		public StringBuilder MBilder = new StringBuilder();

		public Excel2LuaIndex(ExcelReader reader)
		{
			MBilder.Clear();

			// model
			MBilder.AppendLine("language_model = {");
			for (int i = 0; i < reader.Sheets.Length; i++)
			{
				MBilder.AppendLine(string.Format("\t{0} = {1},", reader.Sheets[i].Name, i));
			}
			MBilder.AppendLine("}");
			

			// key
			MBilder.AppendLine("language_key = {");
			for (int i = 0; i < reader.Sheets.Length; i++)
			{
				MBilder.AppendLine(reader.Sheets[i].ToLuaKeyString());
			}
			MBilder.AppendLine("}");

			FilePathUtils.FileWriteAllText(FilePathUtils.Combine(Application.dataPath, "LuaScripts/language.lua"), MBilder.ToString());
		}
	}
}