using System.Runtime.InteropServices.ComTypes;
namespace FastEngine.Core
{
	public struct I18NObject 
	{
	public int model { get; }
	public int key { get; }
	private string _mText;
	public I18NObject(int model, int key)
	{

		this.model = model;
		this.key = key;
		_mText = null;
	}
	public override string ToString() { return _mText ?? (_mText = I18N.Get(model, key)); }
	}
}