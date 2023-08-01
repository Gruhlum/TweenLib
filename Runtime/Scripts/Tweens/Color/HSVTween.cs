using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static HexTecGames.TweenLib.ColorTween;

namespace HexTecGames.TweenLib
{
    [System.Serializable]
    public class HSVTween : ColorTween
    {
        public Mode mode = Mode.Addition;
        public bool useH;
        public bool useS;
        public bool useV;

        public HSVTween() { }
        public HSVTween(HSVTweenData data) : base(data)
        {
            mode = data.mode;
            useH = data.useH;
            useS = data.useS;
            useV = data.useV;
        }

        private Color GenerateColor(float value)
        {
            Color.RGBToHSV(startColor, out float H, out float S, out float V);
            if (mode == Mode.Multiply)
            {
                if (useH)
                    H *= value;
                if (useS)
                    S *= value;
                if (useV)
                    V *= value;
            }
            else
            {
                if (useH)
                    H += value;
                if (useS)
                    S += value;
                if (useV)
                    V += value;
            }
            return Color.HSVToRGB(H, S, V);
        }
        protected override void DoAnimation(float time)
        {
            SetColor(GenerateColor(animationCurve.Evaluate(time)), false);
        }

        protected override TweenData CreateData()
        {
            HSVTweenData data = new HSVTweenData();
            data.mode = mode;
            data.useH = useH;
            data.useS = useS;
            data.useV = useV;
            return data;
        }
    }
    public class HSVTweenData : TweenData
    {
        public Mode mode;
        public bool useH;
        public bool useS;
        public bool useV;

        public override Tween Create()
        {
            return new HSVTween(this);
        }
    }
}