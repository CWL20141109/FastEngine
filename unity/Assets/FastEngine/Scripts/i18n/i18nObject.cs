namespace FastEngine.Core
{
    public struct I18NObject
    {
        public int Model { get; }
        public int Key { get; }
        private string m_text;
        public I18NObject(int model, int key)
        {

            Model = model;
            Key = key;
            m_text = null;
        }

        public override string ToString() { return m_text ?? (m_text = I18N.Get(Model, Key)); }
    }
}