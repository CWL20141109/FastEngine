using System.Collections.Generic;
using FastEngine.Core;
using UnityEngine;

namespace FastEngine.Core
{
    public class I18NConfig : ConfigObject
    {
        public List<SystemLanguage> languages { get; set; }

        protected override void OnInitialize()
        {
            if (languages == null)
            {
                languages = new List<SystemLanguage>();
            }
        }
    }
}