using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace HexTecGames.TweenLib
{
    public abstract class ColorTween : Tween
    {
        private SpriteRenderer sr;
        private Image img;
        private TMP_Text textGUI;
        protected Color startColor;

        protected new ColorTweenData Data;

        public ColorTween(ColorTweenData data, Component component) : base(data, component)
        {
            this.Data = data;
            if (component is SpriteRenderer sr)
            {
                this.sr = sr;
            }
            else if (component is Image img)
            {
                this.img = img;
            }
            else if (component is TMP_Text textGUI)
            {
                this.textGUI = textGUI;
            }
        }
        public override void SetStartData()
        {
            startColor = GetColor();
        }
        public override void ResetEffect()
        {
            SetColor(startColor, false);
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

    [System.Serializable]
    public abstract class ColorTweenData : TweenData
    {

        public override bool CheckForCorrectComponent(Component component)
        {
            if (component is Image || component is SpriteRenderer || component is TMP_Text)
            {
                return true;
            }
            return false;
        }
        public override Component FindCorrectComponent(GameObject go)
        {
            if (go.TryGetComponent(out SpriteRenderer sr))
            {
                return sr;
            }
            else if (go.TryGetComponent(out Image img))
            {
                return img;
            }
            else if (go.TryGetComponent(out TMP_Text text))
            {
                return text;
            }
            else return null;
        }
        protected override Type GetTargetType()
        {
            return typeof(Component);
        }
    }
}