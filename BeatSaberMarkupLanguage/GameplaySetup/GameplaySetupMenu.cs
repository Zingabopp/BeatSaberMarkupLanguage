using BeatSaberMarkupLanguage.Attributes;
using System.Reflection;
using UnityEngine;

namespace BeatSaberMarkupLanguage.GameplaySetup
{
    internal class GameplaySetupMenu
    {
        public string resource;
        public object host;
        public Assembly assembly;

        [UIValue("tab-name")]
        public string name;

        [UIObject("root-tab")]
        private GameObject tabObject;

        public GameplaySetupMenu(string name, string resource, object host, Assembly assembly)
        {
            this.name = name;
            this.resource = resource;
            this.host = host;
            this.assembly = assembly;
        }

        [UIAction("#post-parse")]
        public void Setup()
        {
            BSMLParser.instance.Parse(Utilities.GetResourceContent(assembly, resource), tabObject, host);
        }

        internal void Destroy()
        {
            Logger.log?.Debug($"Destroying tab '{name}'");
            if (tabObject == null)
                return;
            for (int i = 0; i < tabObject.transform.childCount; i++)
            {
                GameObject target = tabObject.transform.GetChild(i).gameObject;
                Logger.log?.Debug($"Destroying '{target.name}'");
                GameObject.Destroy(target);
            }
            //Logger.log?.Debug($"Destroying '{tabObject.name}'");
            //GameObject.Destroy(tabObject);
            tabObject = null; 
            Logger.log?.Debug($"Finished destroying '{name}'");
        }
    }
}
