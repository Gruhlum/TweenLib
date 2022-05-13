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
                    float x = X ? EvaluateCurve(time) * strength : 1;
                    float y = Y ? EvaluateCurve(time) * strength : 1;
                    float z = Z ? EvaluateCurve(time) * strength : 1;

                    transform.position = new Vector3(
                        x == 1 ? transform.position.x : startPosition.x * x,
                        y == 1 ? transform.position.y : startPosition.y * y,
                        z == 1 ? transform.position.z : startPosition.z * z);
                    break;

                case Mode.Addition:
                    x = X ? EvaluateCurve(time) * strength : 0;
                    y = Y ? EvaluateCurve(time) * strength : 0;
                    z = Z ? EvaluateCurve(time) * strength : 0;

                    transform.position = new Vector3(
                        x == 0 ? transform.position.x : startPosition.x + x,
                        y == 0 ? transform.position.y : startPosition.y + y,
                        z == 0 ? transform.position.z : startPosition.z + z);
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