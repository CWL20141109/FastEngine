using System;

namespace FastEngine
{
    public static class EventExtension
    {
        public static bool InvokeGracefully(this Delegate self,params object[] args)
        {
            if (self != null)
            {
                self.DynamicInvoke(args);
                return true;
            }
            return false;
        }
    }
}