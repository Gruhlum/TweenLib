using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace HexTecGames.TweenLib
{
    [System.Serializable]
    public abstract class ColorTween : Tween
    {       
        private SpriteRenderer sr;
        private Image img;
        private TextMeshProUGUI textGUI;
        protected Color startColor;

        public ColorTween() { }
        public ColorTween(TweenData data) : base(data) { }

        public override void Init(GameObject go)
        {
            if (go.TryGetComponent(out sr)) { }
            else if (go.TryGetComponent(out img)) { }
            else if (go.TryGetComponent(out textGUI)) { }
        }
        protected override void SetStartData()
        {           
            startColor = GetColor();
        }
        protected Color GetColor()
        {
            if (sr != null)
                return sr.color;
            else if (img != null)
                return img.color;
            else if (textGUI != null)
                return textGUI.color;
            else return Color.white;
        }
        protected void SetColor(Color col, bool ignoreAlpha)
        {
            if (ignoreAlpha)
            {
                col.a = GetColor().a;
            }
            if (sr != null)
                sr.color = col;
            else if (img != null)
                img.color = col;
            else if (textGUI != null)
                textGUI.color = col;
        }
    }
}