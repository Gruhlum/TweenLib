using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace HexTecGames.TweenLib
{
    public class AlphaTween : ColorTween
    {
        private float startAlpha;

        public AlphaTween() { }

        protected override void SetStartData()
        {
            startAlpha = GetColor().a;
        }

        protected override void DoAnimation(float time)
        {
            AlphaTweenData data = (AlphaTweenData)Data;

            Color col = GetColor();
            if (data.mode == Mode.Addition)
            {
                col.a = startAlpha + Data.animationCurve.Evaluate(time);
            }
            else if (data.mode == Mode.Multiply)
            {
                col.a = startAlpha * Data.animationCurve.Evaluate(time);
            }           
            SetColor(col, false);
        }
    }
    [System.Serializable]
    public class AlphaTweenData : TweenData
    {
        public Mode mode;

        public override Tween Create()
        {
            AlphaTween tween = new AlphaTween();
            tween.Data = this;
            return tween;
        }
    }
}