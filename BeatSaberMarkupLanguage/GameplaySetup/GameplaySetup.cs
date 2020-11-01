using BeatSaberMarkupLanguage.Attributes;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using TMPro;
using UnityEngine;

namespace BeatSaberMarkupLanguage.GameplaySetup
{
    public class GameplaySetup : PersistentSingleton<GameplaySetup>
    {
        private GameplaySetupViewController gameplaySetupViewController;
        private bool setupComplete;

        [UIValue("vanilla-items")]
        private List<Transform> vanillaItems = new List<Transform>();

        [UIValue("mod-menus")]
        private List<object> menus = new List<object>();

        internal void Setup()
        {
            if (menus.Count == 0) return;
            gameplaySetupViewController = Resources.FindObjectsOfTypeAll<GameplaySetupViewController>().First();
            gameplaySetupViewController.transform.Find("HeaderPanel").GetComponentInChildren<TextMeshProUGUI>().fontSize = 4;
            vanillaItems.Clear();
            foreach(Transform transform in gameplaySetupViewController.transform)
            {
                if (transform.name != "HeaderPanel")
                    vanillaItems.Add(transform);
            }
            (gameplaySetupViewController.transform.Find("HeaderPanel") as RectTransform).sizeDelta = new Vector2(90, 6);
            (gameplaySetupViewController.transform.Find("TextSegmentedControl") as RectTransform).anchoredPosition = new Vector2(0, -13);
            setupComplete = true;
            Reload();
        }

        internal void DestroyTabs()
        {
            foreach (var menu in menus)
            {
                if (menu is GameplaySetupMenu gpMenu)
                    gpMenu.Destroy();
            }
        }

        internal void Reload()
        {
            DestroyTabs();
            if (!setupComplete)
                return;
            Logger.log?.Debug($"Reloading GameplayMenu");
            BSMLParser.instance.Parse(Utilities.GetResourceContent(Assembly.GetExecutingAssembly(), "BeatSaberMarkupLanguage.Views.gameplay-setup.bsml"), gameplaySetupViewController.gameObject, this);
        }
        
        public void AddTab(string name, string resource, object host)
        {
            if (menus.Any(x => (x as GameplaySetupMenu).name == name))
                return;
            menus.Add(new GameplaySetupMenu(name, resource, host, Assembly.GetCallingAssembly()));
            //Reload();
        }

        /// <summary>Warning, for now it will not be removed until fresh menu scene reload</summary>
        public void RemoveTab(string name)
        {
            DestroyTabs();
            object match = menus.Where(x => (x as GameplaySetupMenu).name == name).FirstOrDefault();
            if (match is GameplaySetupMenu menu)
            {
                menus.Remove(match);
                menu.Destroy();
            }
            //Reload();
        }
    }
}
