﻿using BeatSaberMarkupLanguage.Components;
using HMUI;
using System;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

namespace BeatSaberMarkupLanguage.Tags
{
    public class ModalColorPickerTag : ModalTag
    {
        public override string[] Aliases => new[] { "modal-color-picker" };

        public override GameObject CreateObject(Transform parent)
        {
            GameObject gameObject = base.CreateObject(parent);
            RectTransform windowTransform = gameObject.transform as RectTransform;
            windowTransform.name = "BSMLModalColorPicker";
            windowTransform.sizeDelta = new Vector2(135, 75);

            ModalColorPicker colorPicker = gameObject.AddComponent<ModalColorPicker>();
            colorPicker.modalView = gameObject.GetComponent<ModalView>();

            RGBPanelController rgbController = GameObject.Instantiate(Resources.FindObjectsOfTypeAll<RGBPanelController>().First(x => x.name == "RGBColorPicker"), gameObject.transform, false);
            rgbController.name = "BSMLRGBPanel";
            (rgbController.gameObject.transform as RectTransform).anchoredPosition = new Vector2(0, 3);
            (rgbController.gameObject.transform as RectTransform).anchorMin = new Vector2(0, 0.25f);
            (rgbController.gameObject.transform as RectTransform).anchorMax = new Vector2(0, 0.25f);
            colorPicker.rgbPanel = rgbController;
            rgbController.colorDidChangeEvent += colorPicker.OnChange;

            HSVPanelController hsvController = GameObject.Instantiate(Resources.FindObjectsOfTypeAll<HSVPanelController>().First(x => x.name == "HSVColorPicker"), gameObject.transform, false);
            hsvController.name = "BSMLHSVPanel";
            (hsvController.gameObject.transform as RectTransform).anchoredPosition = new Vector2(0, 3);
            (hsvController.gameObject.transform as RectTransform).anchorMin = new Vector2(0.75f, 0.5f);
            (hsvController.gameObject.transform as RectTransform).anchorMax = new Vector2(0.75f, 0.5f);
            colorPicker.hsvPanel = hsvController;
            hsvController.colorDidChangeEvent += colorPicker.OnChange;

            Image colorImage = GameObject.Instantiate(Resources.FindObjectsOfTypeAll<ImageView>().First(x => x.gameObject.name == "SaberColorA" && x.transform.parent?.name == "ColorSchemeView"), gameObject.transform, false);
            colorImage.name = "BSMLCurrentColor";
            (colorImage.gameObject.transform as RectTransform).anchoredPosition = new Vector2(0, 0);
            (colorImage.gameObject.transform as RectTransform).anchorMin = new Vector2(0.5f, 0.5f);
            (colorImage.gameObject.transform as RectTransform).anchorMax = new Vector2(0.5f, 0.5f);
            colorPicker.colorImage = colorImage;

            BSMLParser.instance.Parse(@"<horizontal anchor-pos-y='-30' spacing='2' horizontal-fit='PreferredSize'><button text='Cancel' on-click='cancel' pref-width='30'/><action-button text='Done' on-click='done' pref-width='30'/></horizontal>", gameObject, colorPicker);

            return gameObject;
        }
    }
}
