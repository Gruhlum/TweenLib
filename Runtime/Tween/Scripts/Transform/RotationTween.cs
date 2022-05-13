using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace HexTecGames.TweenLib
{
    [System.Serializable]
    public class RotationTween : TransformTween
    {
        public int X;
        public int Y;
        public int Z;

        private Vector3 startRotation;

        public RotationTween()
        {
        }

        public RotationTween(RotationTweenData data) : base(data)
        {
            X = data.X;
            Y = data.Y;
            Z = data.Z;
        }

        public override void Init(GameObject go)
        {
            base.Init(go);
            startRotation = go.transform.eulerAngles;
        }
        private float CalculateAngle(float keyValue)
        {
            return Mathf.LerpUnclamped(0f, 360f, keyValue);
        }
        protected override void DoAnimation(float time)
        {
            transform.transform.eulerAngles = startRotation + new Vector3(0, 0, CalculateAngle(EvaluateCurve(time)));
        }

        protected override TweenData CreateData()
        {
            RotationTweenData data = new RotationTweenData();
            data.X = X;
            data.Y = Y;
            data.Z = Z;
            return data;
        }
    }
    public class RotationTweenData : TweenData
    {
        public int X;
        public int Y;
        public int Z;

        public override Tween Create()
        {
            return new RotationTween(this);
        }
    }
}