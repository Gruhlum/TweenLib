using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static HexTecGames.TweenLib.ColorTween;
using static HexTecGames.TweenLib.Tween;

namespace HexTecGames.TweenLib
{
    [System.Serializable]
    public class AlphaTween : ColorTween
    {
        public Mode mode = Mode.Addition;
        private float startAlpha;

        public AlphaTween()
        {
        }

        public AlphaTween(AlphaTweenData data) : base(data)
        {
            mode = data.mode;
        }

        public override void Init(GameObject go)
        {
            base.Init(go);
            startAlpha = GetColor().a;
        }

        protected override TweenData CreateData()
        {
            AlphaTweenData data = new AlphaTweenData();
            data.mode = mode;
            return data;
        }

        protected override void DoAnimation(float time)
        {
            Color col = GetColor();
            if (mode == Mode.Addition)
            {
                col.a = startAlpha + animationCurve.Evaluate(time);
            }
            else if (mode == Mode.Multiply)
            {
                col.a = startAlpha * animationCurve.Evaluate(time);
            }           
            SetColor(col, false);
        }
    }
    public class AlphaTweenData : TweenData
    {
        public Mode mode;

        public override Tween Create()
        {
            return new AlphaTween(this);
        }
    }
}