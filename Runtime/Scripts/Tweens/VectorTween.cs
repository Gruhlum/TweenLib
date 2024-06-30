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

                    if (data.X) result.x = startVector.x * EvaluateCurve(time) * data.strength - currentVec.x;
                    if (data.Y) result.y = startVector.y * EvaluateCurve(time) * data.strength - currentVec.y;
                    if (data.Z) result.z = startVector.z * EvaluateCurve(time) * data.strength - currentVec.z;
                    break;

                case Mode.Addition:
                    if (data.X) result.x = startVector.x + EvaluateCurve(time) * data.strength - currentVec.x;
                    if (data.Y) result.y = startVector.y + EvaluateCurve(time) * data.strength - currentVec.y;
                    if (data.Z) result.z = startVector.z + EvaluateCurve(time) * data.strength - currentVec.z;
                    break;

                case Mode.Set:
                    if (data.X) result.x = EvaluateCurve(time) * data.strength - currentVec.x;
                    if (data.Y) result.y = EvaluateCurve(time) * data.strength - currentVec.y;
                    if (data.Z) result.z = EvaluateCurve(time) * data.strength - currentVec.z;
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