using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace HexTecGames.TweenLib
{
    [System.Serializable]
    public class PositionTween : TransformTween
    {
        private Vector3 startPosition;
        public bool X;
        public bool Y;
        public bool Z;
        public Mode Mode;
        public float strength = 1f;

        public PositionTween() { }
        public PositionTween(PositionData data) : base(data)
        {
            X = data.X;
            Y = data.Y;
            Z = data.Z;
            Mode = data.Mode;
            this.strength = data.strength;
        }

        protected override void DoAnimation(float time)
        {
            switch (Mode)
            {
                case Mode.Multiply:
                    float? x = X ? EvaluateCurve(time) * strength : null;
                    float? y = Y ? EvaluateCurve(time) * strength : null;
                    float? z = Z ? EvaluateCurve(time) * strength : null;

                    transform.position = new Vector3(
                        x.HasValue ? startPosition.x * x.Value : transform.position.x,
                        y.HasValue ? startPosition.y * y.Value : transform.position.y,
                        z.HasValue ? startPosition.z * z.Value : transform.position.z);
                    break;

                case Mode.Addition:
                    x = X ? EvaluateCurve(time) * strength : null;
                    y = Y ? EvaluateCurve(time) * strength : null;
                    z = Z ? EvaluateCurve(time) * strength : null;

                    transform.position = new Vector3(
                       x.HasValue ? startPosition.x + x.Value : transform.position.x,
                       y.HasValue ? startPosition.y + y.Value : transform.position.y,
                       z.HasValue ? startPosition.z + z.Value : transform.position.z);
                    break;
                default:
                    break;
            }
        }

        public override void Init(GameObject go)
        {
            base.Init(go);
            startPosition = go.transform.position;
        }

        protected override TweenData CreateData()
        {
            PositionData data = new PositionData();
            data.X = X;
            data.Y = Y;
            data.Z = Z;
            data.Mode = Mode;
            data.strength = strength;
            return data;
        }
    }
    [System.Serializable]
    public class PositionData : TweenData
    {
        public bool X;
        public bool Y;
        public bool Z;
        public Mode Mode;
        public float strength;

        public override Tween Create()
        {
            return new PositionTween(this);
        }
    }
}