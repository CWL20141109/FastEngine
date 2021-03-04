/*
* @Author: cwl
* @Description:
* @Date: 2021-03-01 15:09:02
*/

namespace FastEngine.Core.Excel2Table
{
	public abstract class Excel2Any
	{
		protected ExcelReader reader;
		public Excel2Any(ExcelReader reader)
		{
			this.reader = reader;
		}
	}
}