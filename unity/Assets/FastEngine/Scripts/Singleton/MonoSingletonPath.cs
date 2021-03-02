using System;

namespace FastEngine
{
    [AttributeUsage(AttributeTargets.Class)]
    public class MonoSingletonPath : Attribute
    {
        public string PathInHierarchy { get; private set; }

        public MonoSingletonPath(string pathInHierarchy)
        {
            this.PathInHierarchy = pathInHierarchy;
        }
    }
}