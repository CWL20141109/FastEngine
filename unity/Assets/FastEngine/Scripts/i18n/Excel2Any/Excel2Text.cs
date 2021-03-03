namespace FastEngine.Core.I18n
{
	public class Excel2Text
	{
		public Excel2Text(ExcelReader reader)
		{
			FilePathUtils.DirectoryClean(AppUtils.I18NDataDirectory());

			for (int i = 0; i < reader.Sheets.Length; i++)
			{
				var sheet = reader.Sheets[i];
				for (int l = 0; l < reader.Options.Languages.Count; l++)
				{
					var language = reader.Options.Languages[l];

					FilePathUtils.FileWriteAllText(FilePathUtils.Combine(AppUtils.I18NDataDirectory(), language.ToString(), i.ToString() + ".txt"), sheet.ToValueString(l + 1));
				}
			}
		}
	}
}