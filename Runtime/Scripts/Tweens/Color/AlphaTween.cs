using UnityEngine;

namespace HexTecGames.TweenLib
{
    public class AlphaTween : ColorTween
    {
        private float startAlpha;

        public AlphaTween(AlphaTweenData data, Component component) : base(data, component)
        { }

        public override void SetStartData()
        {
            startAlpha = GetColor().a;
            base.SetStartData();
        }

        protected override void DoAnimation(float time)
        {
            AlphaTweenData data = (AlphaTweenData)Data;

            Color col = GetColor();
            if (data.mode == Mode.Addition)
            {
                col.a = startAlpha + GetAnimationCurveValue(time);
            }
            else if (data.mode == Mode.Multiply)
            {
                col.a = startAlpha * GetAnimationCurveValue(time);
            }
            else if (data.mode == Mode.Set)
            {
                col.a = GetAnimationCurveValue(time);
            }
            SetColor(col, false);
        }
    }
    [System.Serializable]
    public class AlphaTweenData : ColorTweenData
    {
        public Mode mode;

        public override Tween Create(Component component)
        {
            AlphaTween tween = new AlphaTween(this, component);
            return tween;
        }
    }
}