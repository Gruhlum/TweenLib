using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace HexTecGames.TweenLib
{
    public abstract class VectorTween : Tween
    {
        protected Vector3 startVector;
        protected VectorData data;

        public VectorTween(VectorData data) : base(data)
        {
            this.data = data;
        }
        protected Vector3 CalculateVector(float time, Vector3 currentVec)
        {
            Vector3 result = currentVec;
            switch (data.Mode)
            {
                case Mode.Multiply:

                    if (data.X) result.x = startVector.x * GetAnimationCurveValue(time) * data.strength;
                    if (data.Y) result.y = startVector.y * GetAnimationCurveValue(time) * data.strength;
                    if (data.Z) result.z = startVector.z * GetAnimationCurveValue(time) * data.strength;
                    break;

                case Mode.Addition:
                    if (data.X) result.x = startVector.x + GetAnimationCurveValue(time) * data.strength;
                    if (data.Y) result.y = startVector.y + GetAnimationCurveValue(time) * data.strength;
                    if (data.Z) result.z = startVector.z + GetAnimationCurveValue(time) * data.strength;
                    break;

                case Mode.Set:
                    if (data.X) result.x = GetAnimationCurveValue(time) * data.strength;
                    if (data.Y) result.y = GetAnimationCurveValue(time) * data.strength;
                    if (data.Z) result.z = GetAnimationCurveValue(time) * data.strength;
                    break;
                default:
                    break;
            }
            return result;
        }
    }
    public abstract class VectorData : TweenData
    {
        public bool X;
        public bool Y;
        public bool Z;
        public Mode Mode;
        public float strength = 1;
    }
}