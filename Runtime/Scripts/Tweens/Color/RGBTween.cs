using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace HexTecGames.TweenLib
{
    [System.Serializable]
    public class RGBTween : ColorTween
    {
        public Color color = Color.white;

        public RGBTween() { }
        public RGBTween(RGBTweenData data) : base(data)
        {
            color = data.color;
        }

        protected override TweenData CreateData()
        {
            RGBTweenData data = new RGBTweenData();
            data.color = color;
            return data;
        }

        protected override void DoAnimation(float time)
        {
            SetColor(Color.Lerp(startColor, color, animationCurve.Evaluate(time)), true);
        }
    }
    public class RGBTweenData : TweenData
    {
        public Color color;

        public override Tween Create()
        {
            return new RGBTween(this);
        }
    }
}