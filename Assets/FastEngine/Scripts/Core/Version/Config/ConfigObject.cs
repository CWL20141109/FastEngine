using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace FastEngine.Core
{

    public class ConfigObject
    {
        public void Initialize() { }

        protected virtual void OnInitialize() { }

        public void Save<T>() where T :ConfigObject,new ()
        {
           
        }
    }
}

