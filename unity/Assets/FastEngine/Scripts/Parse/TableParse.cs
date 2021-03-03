/*
* @Author: cwl
* @Description: table 解析
* @Date: 2021-03-03 23:39:22
*/


namespace FastEngine.Core.Excel2Table
{
    /// <summary>
    /// table 解析
    /// </summary>
    public abstract class TableParse
    {
        protected string TableName;
        protected FormatOptions Format;
        protected string Content;

        protected TableParse(string tableName, FormatOptions format)
        {
            TableName = tableName;
            Format = format;
        }

        protected void LoadAsset()
        {
            if (!string.IsNullOrEmpty(Content)) return;
            if (App.RunModel == AppRunModel.Develop )
            {
                var filePath = FilePathUtils.Combine(AppUtils.TableDataDirectory(), TableName + ".csv");
                bool succeed = false;
                Content = FilePathUtils.FileReadAllText(filePath, out succeed);
            }
            else
            {
                var loader = AssetBundleLoader.Allocate(AppUtils.TableDataBundleRootDirectory(), "Table","Data");
            }
                
            
        }
    }
}