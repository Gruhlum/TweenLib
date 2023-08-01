using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace HexTecGames.TweenLib
{
    [System.Serializable]
    public abstract class TransformTween : Tween
    {
        public bool X;
        public bool Y;
        public bool Z;
        public Mode Mode;
        public float strength = 1;

        protected Vector3 startVec;
        protected Transform targetTransform;

        public TransformTween() { }
        protected TransformTween(TransformData data) : base(data)
        {
            X = data.X;
            Y = data.Y;
            Z = data.Z;
            Mode = data.Mode;
            this.strength = data.strength;
        }
        protected Vector3 CalculateVector(float time)
        {
            Vector3 result = startVec;
            switch (Mode)
            {
                case Mode.Multiply:

                    if (X) result.x *=  EvaluateCurve(time) * strength;
                    if (Y) result.y *=  EvaluateCurve(time) * strength;
                    if (Z) result.z *=  EvaluateCurve(time) * strength;
                    break;

                case Mode.Addition:
                    if (X) result.x += EvaluateCurve(time) * strength;
                    if (Y) result.y += EvaluateCurve(time) * strength;
                    if (Z) result.z += EvaluateCurve(time) * strength;
                    break;

                case Mode.Set:
                    if (X) result.x = EvaluateCurve(time) * strength;
                    if (Y) result.y = EvaluateCurve(time) * strength;
                    if (Z) result.z = EvaluateCurve(time) * strength;
                    break;
                default:
                    break;
            }
            return result;
        }

        public override void Init(GameObject go)
        {
            targetTransform = go.transform;
            base.Init(go);
        }

        public override TweenData GetData()
        {
            TransformData data = base.GetData() as TransformData;
            data.X = X;
            data.Y = Y;
            data.Z = Z;
            data.Mode = Mode;
            data.strength = strength;
            return data;
        }
    }
    public abstract class TransformData : TweenData
    {
        public bool X;
        public bool Y;
        public bool Z;
        public Mode Mode;
        public float strength;
    }
}