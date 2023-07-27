using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace HexTecGames.TweenLib
{
    [System.Serializable]
    public class ScaleTween : TransformTween
    {
        private Vector3 startSize;
        public bool X;
        public bool Y;
        public bool Z;
        public Mode Mode;
        public float strength = 1;

        public ScaleTween()
        {
        }

        public ScaleTween(ScaleTweenData data) : base(data)
        {
            X = data.X;
            Y = data.Y;
            Z = data.Z;
            Mode = data.Mode;
            strength = data.strength;
        }

        public override void Init(GameObject go)
        {
            base.Init(go);
            startSize = go.transform.localScale;
        }
        protected override void DoAnimation(float time)
        {
            switch (Mode)
            {
                case Mode.Multiply:
                    float? x = X ? EvaluateCurve(time) * strength : null;
                    float? y = Y ? EvaluateCurve(time) * strength : null;
                    float? z = Z ? EvaluateCurve(time) * strength : null;
                    transform.localScale = new Vector3(
                        x.HasValue ? startSize.x * x.Value : transform.localScale.x,
                        y.HasValue ? startSize.y * y.Value : transform.localScale.y,
                        z.HasValue ? startSize.z * z.Value : transform.localScale.z);
                    break;

                case Mode.Addition:
                    x = X ? EvaluateCurve(time) * strength : null;
                    y = Y ? EvaluateCurve(time) * strength : null;
                    z = Z ? EvaluateCurve(time) * strength : null;
                    transform.localScale = new Vector3(
                        x.HasValue ? startSize.x + x.Value : transform.localScale.x,
                        y.HasValue ? startSize.y + y.Value : transform.localScale.y,
                        z.HasValue ? startSize.z + z.Value : transform.localScale.z);
                    break;
                default:
                    break;
            }
           
        }

        protected override TweenData CreateData()
        {
            ScaleTweenData data = new ScaleTweenData();
            data.X = X;
            data.Y = Y;
            data.Z = Z;
            data.Mode = Mode;
            data.strength = strength;
            return data;
        }
    }
    public class ScaleTweenData : TweenData
    {
        public bool X;
        public bool Y;
        public bool Z;
        public Mode Mode;
        public float strength;

        public override Tween Create()
        {
            return new ScaleTween(this);
        }
    }
}