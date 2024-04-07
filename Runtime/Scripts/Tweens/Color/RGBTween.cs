using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace HexTecGames.TweenLib
{
    public class RGBTween : ColorTween
    {
        public RGBTween(RGBTweenData data) : base(data)
        { }

        protected override void DoAnimation(float time)
        {
            RGBTweenData data = (RGBTweenData)Data;
            SetColor(Color.Lerp(startColor, data.color, data.animationCurve.Evaluate(time)), true);
        }
    }
    [System.Serializable]
    public class RGBTweenData : TweenData
    {
        public Color color;

        public override Tween Create()
        {
            RGBTween tween = new RGBTween(this);
            return tween;
        }
    }
}