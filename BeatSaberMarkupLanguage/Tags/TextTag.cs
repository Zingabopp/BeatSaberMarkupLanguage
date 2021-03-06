﻿using BeatSaberMarkupLanguage.Components;
using System.Linq;
using TMPro;
using UnityEngine;

namespace BeatSaberMarkupLanguage.Tags
{
    public class TextTag : BSMLTag
    {
        public override string[] Aliases => new[] { "text", "label" };

        private TMP_FontAsset font;
        public override GameObject CreateObject(Transform parent)
        {
            GameObject gameObj = new GameObject("BSMLText");
            gameObj.transform.SetParent(parent, false);

            FormattableText textMesh = gameObj.AddComponent<FormattableText>();
            if (font == null)
                font = Resources.FindObjectsOfTypeAll<TMP_FontAsset>().First(t => t.name == "Teko-Medium SDF No Glow");
            textMesh.font = MonoBehaviour.Instantiate(font);
            textMesh.fontSize = 4;
            textMesh.color = Color.white;

            textMesh.rectTransform.anchorMin = new Vector2(0.5f, 0.5f);
            textMesh.rectTransform.anchorMax = new Vector2(0.5f, 0.5f);

            return gameObj;
        }
    }
}
