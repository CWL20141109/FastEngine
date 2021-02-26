/*
* @Author: cwl
* @Description:
* @Date: 2021-02-26 星期五 15:47:01
*/

using UnityEditor;
namespace FastEngine.Editor.Excel2Table
{
	public class TableEditor
	{
		[MenuItem("FastEngine/Table -> 打开配置",false,600)]
		static void OpenConfig()
		{
			FastEditorWindow.ShowWindow<TableEditorWindow>();
		}
	}
}