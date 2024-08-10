using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace HexTecGames.TweenLib
{
    public class RGBTween : ColorTween
    {
        private new RGBTweenData Data;

        public RGBTween(RGBTweenData data, Component component) : base(data, component)
        {
            this.Data = data;
        }

        protected override void DoAnimation(float time)
        {
            //Debug.Log(startColor + " - " + Data.color + " - " + GetAnimationCurveValue(time));
            SetColor(Color.Lerp(startColor, Data.color, GetAnimationCurveValue(time)), Data.ignoreAlpha);
        }
    }
    [System.Serializable]
    public class RGBTweenData : ColorTweenData
    {
        public Color color = Color.white;
        public bool ignoreAlpha = true;

        public override Tween Create(Component component)
        {
            RGBTween tween = new RGBTween(this, component);
            return tween;
        }
    }
}