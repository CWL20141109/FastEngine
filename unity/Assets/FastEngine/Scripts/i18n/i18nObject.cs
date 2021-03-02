namespace FastEngine.Core
{
	public class I18NObject
	{
		public int Model { get; }
		public int Key { get; }

		public I18NObject(int model, int key)
		{
			this.Model = model;
			this.Key = key;
		}

		public override string ToString() { return I18N.Get(Model, Key); }
	}
}