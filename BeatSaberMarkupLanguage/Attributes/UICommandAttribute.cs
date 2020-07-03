using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BeatSaberMarkupLanguage.Attributes
{
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = false, Inherited = true)]
    public sealed class UICommandAttribute : Attribute
    {
        public string id;
        public UICommandAttribute(string id)
        {
            this.id = id;
        }
    }
}
