namespace FastEngine.Core
{
	public struct I18NObject
	{
		public int Model { get; }
		public int Key { get; }

		public I18NObject(int model, int key)
		{

			Model = model;
			Key = key;
		}

		public override string ToString() { return I18N.Get(Model, Key); }
	}
}