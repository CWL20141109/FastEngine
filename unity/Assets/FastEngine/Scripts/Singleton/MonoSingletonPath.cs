using System;

namespace FastEngine
{
    [AttributeUsage(AttributeTargets.Class)]
    public class MonoSingletonPath : Attribute
    {
        public string pathInHierarchy { get; private set; }

        public MonoSingletonPath(string pathInHierarchy)
        {
            this.pathInHierarchy = pathInHierarchy;
        }
    }
}