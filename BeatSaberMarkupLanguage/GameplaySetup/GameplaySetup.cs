﻿using BeatSaberMarkupLanguage.Attributes;
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
            BSMLParser.instance.Parse(Utilities.GetResourceContent(Assembly.GetExecutingAssembly(), "BeatSaberMarkupLanguage.Views.gameplay-setup.bsml"), gameplaySetupViewController.gameObject, this);

            gameplaySetupViewController.didActivateEvent += GameplaySetupDidActivate;
        }

        private void GameplaySetupDidActivate(bool firstActivation, bool addedToHierarchy, bool screenSystemEnabling)
        {
            MenuType menuType;
            switch (BeatSaberUI.MainFlowCoordinator.YoungestChildFlowCoordinatorOrSelf())
            {
                case CampaignFlowCoordinator _:
                    menuType = MenuType.Campaign;
                    break;
                case SinglePlayerLevelSelectionFlowCoordinator _:
                    menuType = MenuType.Solo;
                    break;
                case HostGameServerLobbyFlowCoordinator _:
                case QuickPlayLobbyFlowCoordinator _:
                case GameServerLobbyFlowCoordinator _:
                    menuType = MenuType.Online;
                    break;
                default:
                    throw new System.Exception("Unhandled menu type");
                    break;
            }
            foreach (GameplaySetupMenu menu in menus)
                menu.SetVisible(menu.IsMenuType(menuType));
            /*switch (BeatSaberUI.MainFlowCoordinator.YoungestChildFlowCoordinatorOrSelf().GetType())
            {
                case typeof(CampaignFlowCoordinator):
                    break;
            }*/
        }

        public void AddTab(string name, string resource, object host)
        {
            AddTab(name, resource, host, MenuType.All);
        }

        public void AddTab(string name, string resource, object host, MenuType menuType)
        {
            if (menus.Any(x => (x as GameplaySetupMenu).name == name))
                return;
            menus.Add(new GameplaySetupMenu(name, resource, host, Assembly.GetCallingAssembly(), menuType));
        }

        /// <summary>Warning, for now it will not be removed until fresh menu scene reload</summary>
        public void RemoveTab(string name)
        {
            IEnumerable<object> menu = menus.Where(x => (x as GameplaySetupMenu).name == name);
            if (menu.Count() > 0)
                menus.Remove(menu.FirstOrDefault());
        }
    }
}
