using System.Collections;
using System.Collections.Generic;
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

        public ColorTween(ColorTweenData data) : base(data)
        {
            this.Data = data;
            if (data.component is SpriteRenderer sr)
            {
                this.sr = sr;
            }
            else if (data.component is Image img)
            {
                this.img = img;
            }
            else if (data.component is TMP_Text textGUI)
            {
                this.textGUI = textGUI;
            }
        }
        //protected override void SetStartObject(Component component)
        //{
        //    if (component is SpriteRenderer sr)
        //    {
        //        this.sr = sr;
        //    }
        //    else if (component is Image img)
        //    {
        //        this.img = img;
        //    }
        //    else if (component is TMP_Text textGUI)
        //    {
        //        this.textGUI = textGUI;
        //    }
        //}
        //protected override void SetStartObject(GameObject go)
        //{
        //    if (Data.ComponentTarget == ComponentTarget.Current)
        //    {
        //        GetComponentFromGameObject(go);
        //    }
        //    else if (Data.ComponentTarget == ComponentTarget.Parent)
        //    {
        //        GetComponentFromGameObject(go.transform.parent.gameObject);
        //    }
        //    else if (Data.ComponentTarget == ComponentTarget.Child)
        //    {
        //        for (int i = 0; i < go.transform.childCount; i++)
        //        {
        //            if (GetComponentFromGameObject(go.transform.GetChild(i).gameObject))
        //            {
        //                return;
        //            }
        //        }
        //    }
        //}

        //private bool GetComponentFromGameObject(GameObject go)
        //{
        //    if (Data.ColorTarget == ColorTarget.Image)
        //    {
        //        return go.TryGetComponent(out img);
        //    }
        //    else if (Data.ColorTarget == ColorTarget.SpriteRenderer)
        //    {
        //        return go.TryGetComponent(out sr);
        //    }
        //    else if (Data.ColorTarget == ColorTarget.TMP_Text)
        //    {
        //        return go.TryGetComponent(out textGUI);
        //    }
        //    return false;
        //}

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
        public Component component;

        public override void GetRequiredComponent(GameObject go)
        {
            if (component != null)
            {
                if (component is SpriteRenderer || component is Image || component is TMP_Text)
                {
                    Debug.Log("Wrong component type: Has to be 'SpriteRenderer', 'Image' or 'TMP_Text'", go);
                    return;
                }
            }
            else
            {
                FindComponent(go);
            }
        }

        private void FindComponent(GameObject go)
        {
            if (go.TryGetComponent(out SpriteRenderer sr))
            {
                component = sr;
            }
            else if (go.TryGetComponent(out Image img))
            {
                component = img;
            }
            else if (go.TryGetComponent(out TMP_Text text))
            {
                component = text;
            }
        }
        //public ColorTarget ColorTarget;
        //public ComponentTarget ComponentTarget;
    }
}