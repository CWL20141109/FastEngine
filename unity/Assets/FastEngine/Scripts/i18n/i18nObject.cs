namespace FastEngine.Core
{
	public class i18nObject
	{
		public int model { get; }
		public int key { get; }

		public i18nObject(int model, int key)
		{
			this.model = model;
			this.key = key;
		}

		public override string ToString() { return i18n.Get(model, key); }
	}
}