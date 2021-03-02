using System.Text;
using UnityEngine;

namespace FastEngine.Core.I18n
{
	public class Excel2LuaIndex
	{
		public StringBuilder mBilder = new StringBuilder();

		public Excel2LuaIndex(ExcelReader reader)
		{
			mBilder.Clear();

			// model
			mBilder.AppendLine("language_model = {");
			for (int i = 0; i < reader.Sheets.Length; i++)
			{
				mBilder.AppendLine(string.Format("\t{0} = {1},", reader.Sheets[i].name, i));
			}
			mBilder.AppendLine("}");
			

			// key
			mBilder.AppendLine("language_key = {");
			for (int i = 0; i < reader.Sheets.Length; i++)
			{
				mBilder.AppendLine(reader.Sheets[i].ToLuaKeyString());
			}
			mBilder.AppendLine("}");

			FilePathUtils.FileWriteAllText(FilePathUtils.Combine(Application.dataPath, "LuaScripts/language.lua"), mBilder.ToString());
		}
	}
}