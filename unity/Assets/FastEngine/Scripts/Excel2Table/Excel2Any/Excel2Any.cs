/*
* @Author: cwl
* @Description:
* @Date: 2021-03-01 15:09:02
*/

namespace FastEngine.Core.Excel2Table
{
	public abstract class Excel2Any
	{
		protected ExcelReader Reader;
		public Excel2Any(ExcelReader reader)
		{
			this.Reader = reader;
		}
	}
}