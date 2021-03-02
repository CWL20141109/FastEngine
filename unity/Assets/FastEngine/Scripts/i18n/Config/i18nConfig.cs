using System.Collections.Generic;
using FastEngine.Core;
using UnityEngine;

namespace FastEngine.Core
{
    public class I18NConfig : ConfigObject
    {
        public List<SystemLanguage> Languages { get; set; }

        protected override void OnInitialize()
        {
            if (Languages == null)
            {
                Languages = new List<SystemLanguage>();
            }
        }
    }
}