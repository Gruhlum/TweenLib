using HexTecGames.Basics;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace HexTecGames.TweenLib
{
    public abstract class VectorTween : Tween
    {
        protected Vector3 startVector;
        protected VectorData data;

        public VectorTween(VectorData data, Component component) : base(data, component)
        {
            this.data = data;
        }
        protected Vector3 CalculateVector(float time, Vector3 currentVec)
        {
            Vector3 result = currentVec;
            switch (data.mode)
            {
                case Mode.Multiply:

                    if (data.X) result.x = startVector.x + startVector.x * GetAnimationCurveValue(time) * data.strength * data.multiplierX;
                    if (data.Y) result.y = startVector.y + startVector.y * GetAnimationCurveValue(time) * data.strength * data.multiplierY;
                    if (data.Z) result.z = startVector.z + startVector.z * GetAnimationCurveValue(time) * data.strength * data.multiplierZ;
                    break;

                case Mode.Addition:
                    if (data.X) result.x = startVector.x + GetAnimationCurveValue(time) * data.strength * data.multiplierX;
                    if (data.Y) result.y = startVector.y + GetAnimationCurveValue(time) * data.strength * data.multiplierY;
                    if (data.Z) result.z = startVector.z + GetAnimationCurveValue(time) * data.strength * data.multiplierZ;
                    break;

                case Mode.Set:
                    if (data.X) result.x = GetAnimationCurveValue(time) * data.strength * data.multiplierX;
                    if (data.Y) result.y = GetAnimationCurveValue(time) * data.strength * data.multiplierY;
                    if (data.Z) result.z = GetAnimationCurveValue(time) * data.strength * data.multiplierZ;
                    break;
                default:
                    break;
            }
            return result;
        }
    }
    public abstract class VectorData : TweenData
    {
        [Space]
        public Mode mode;
        public float strength = 1;

        public bool X;
        [DrawIf(nameof(X), true)] public float multiplierX = 1;
        public bool Y;
        [DrawIf(nameof(Y), true)] public float multiplierY = 1;
        public bool Z;
        [DrawIf(nameof(Z), true)] public float multiplierZ = 1;
    }
}