using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace HexTecGames.TweenLib
{
    public class HSVTween : ColorTween
    {
        public HSVTween(HSVTweenData data, Component component) : base(data, component)
        { }

        private Color GenerateColor(float value)
        {
            HSVTweenData data = (HSVTweenData)Data;

            Color.RGBToHSV(startColor, out float H, out float S, out float V);
            if (data.mode == Mode.Multiply)
            {
                if (data.useH)
                    H *= value;
                if (data.useS)
                    S *= value;
                if (data.useV)
                    V *= value;
            }
            else
            {
                if (data.useH)
                    H += value;
                if (data.useS)
                    S += value;
                if (data.useV)
                    V += value;
            }
            return Color.HSVToRGB(H, S, V);
        }
        protected override void DoAnimation(float time)
        {
            SetColor(GenerateColor(GetAnimationCurveValue(time)), false);
        }
    }
    [System.Serializable]
    public class HSVTweenData : ColorTweenData
    {
        public Mode mode;
        public bool useH;
        public bool useS;
        public bool useV;

        public override Tween Create(Component component)
        {
            HSVTween tween = new HSVTween(this, component);
            return tween;
        }
    }
}