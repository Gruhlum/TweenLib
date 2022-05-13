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
                    float x = X ? EvaluateCurve(time) * strength : 1;
                    float y = Y ? EvaluateCurve(time) * strength : 1;
                    float z = Z ? EvaluateCurve(time) * strength : 1;

                    transform.localScale = new Vector3(
                        x == 1 ? transform.localScale.x : startSize.x * x,
                        y == 1 ? transform.localScale.y : startSize.y * y,
                        z == 1 ? transform.localScale.z : startSize.z * z);
                    break;

                case Mode.Addition:
                    x = X ? EvaluateCurve(time) * strength : 0;
                    y = Y ? EvaluateCurve(time) * strength : 0;
                    z = Z ? EvaluateCurve(time) * strength : 0;

                    transform.localScale = new Vector3(
                        x == 0 ? transform.localScale.x : startSize.x + x,
                        y == 0 ? transform.localScale.y : startSize.y + y,
                        z == 0 ? transform.localScale.z : startSize.z + z);
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