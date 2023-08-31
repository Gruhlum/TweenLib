using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace HexTecGames.TweenLib
{
    public abstract class TransformTween : Tween
    {
        protected Vector3 startVec;
        protected Transform targetTransform;

        public TransformTween() { }

        protected Vector3 CalculateVector(float time)
        {
            TransformData data = (TransformData)Data;
            Vector3 result = startVec;
            switch (data.Mode)
            {
                case Mode.Multiply:

                    if (data.X) result.x *=  EvaluateCurve(time) * data.strength;
                    if (data.Y) result.y *=  EvaluateCurve(time) * data.strength;
                    if (data.Z) result.z *=  EvaluateCurve(time) * data.strength;
                    break;

                case Mode.Addition:
                    if (data.Y) result.y += EvaluateCurve(time) * data.strength;
                    if (data.X) result.x += EvaluateCurve(time) * data.strength;
                    if (data.Z) result.z += EvaluateCurve(time) * data.strength;
                    break;

                case Mode.Set:
                    if (data.X) result.x = EvaluateCurve(time) * data.strength;
                    if (data.Y) result.y = EvaluateCurve(time) * data.strength;
                    if (data.Z) result.z = EvaluateCurve(time) * data.strength;
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