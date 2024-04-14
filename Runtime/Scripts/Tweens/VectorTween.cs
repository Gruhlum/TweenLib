using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace HexTecGames.TweenLib
{
    public abstract class VectorTween : Tween
    {
        protected Vector3 startVec;
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

                    if (data.X) result.x = startVec.x * EvaluateCurve(time) * data.strength;
                    if (data.Y) result.y = startVec.y * EvaluateCurve(time) * data.strength;
                    if (data.Z) result.z = startVec.z * EvaluateCurve(time) * data.strength;
                    break;

                case Mode.Addition:
                    if (data.X) result.x = startVec.x + EvaluateCurve(time) * data.strength;
                    if (data.Y) result.y = startVec.y + EvaluateCurve(time) * data.strength;
                    if (data.Z) result.z = startVec.z + EvaluateCurve(time) * data.strength;
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