using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace HexTecGames.TweenLib
{
    [System.Serializable]
    public static class AnimationData
    {
        const float c1 = 1.70158f;
        const float c2 = c1 * 1.525f;
        const float c3 = c1 + 1f;
        const float c4 = (2f * MathF.PI) / 3f;
        const float c5 = (2f * MathF.PI) / 4.5f;

        const float n1 = 7.5625f;
        const float d1 = 2.75f;

        public static Func<float, float> GetFunction(AnimationType type, Curve curve)
        {
            switch (type)
            {
                case AnimationType.EaseIn:
                    switch (curve)
                    {
                        case Curve.Sine:
                            return EaseInSine;
                        case Curve.Quad:
                            return EaseInQuad;
                        case Curve.Cubic:
                            return EaseInCubic;
                        case Curve.Quart:
                            return EaseInQuart;
                        case Curve.Quint:
                            return EaseInQuint;
                        case Curve.Expo:
                            return EaseInExpo;
                        case Curve.Circ:
                            return EaseInCirc;
                        case Curve.Back:
                            return EaseInBack;
                        case Curve.Elastic:
                            return EaseInElastic;
                        case Curve.Bounce:
                            return EaseInBounce;
                        default:
                            return null;
                    }
                case AnimationType.EaseOut:
                    switch (curve)
                    {
                        case Curve.Sine:
                            return EaseOutSine;
                        case Curve.Quad:
                            return EaseOutQuad;
                        case Curve.Cubic:
                            return EaseOutCubic;
                        case Curve.Quart:
                            return EaseOutQuart;
                        case Curve.Quint:
                            return EaseOutQuint;
                        case Curve.Expo:
                            return EaseOutExpo;
                        case Curve.Circ:
                            return EaseOutCirc;
                        case Curve.Back:
                            return EaseOutBack;
                        case Curve.Elastic:
                            return EaseOutElastic;
                        case Curve.Bounce:
                            return EaseOutBounce;
                        default:
                            return null;
                    }
                case AnimationType.EaseInOut:
                    switch (curve)
                    {
                        case Curve.Sine:
                            return EaseInOutSine;
                        case Curve.Quad:
                            return EaseInOutQuad;
                        case Curve.Cubic:
                            return EaseInOutCubic;
                        case Curve.Quart:
                            return EaseInOutQuart;
                        case Curve.Quint:
                            return EaseInOutQuint;
                        case Curve.Expo:
                            return EaseInOutExpo;
                        case Curve.Circ:
                            return EaseInOutCirc;
                        case Curve.Back:
                            return EaseInOutBack;
                        case Curve.Elastic:
                            return EaseInOutElastic;
                        case Curve.Bounce:
                            return EaseInOutBounce;
                        default:
                            return null;
                    }
                default:
                    return null;
            }
        }

        public static float EaseInSine(float x)
        {
            return 1f - Mathf.Cos((x * Mathf.PI) / 2f);
        }
        public static float EaseOutSine(float x)
        {
            return Mathf.Sin((x * Mathf.PI) / 2f);
        }
        public static float EaseInOutSine(float x)
        {
            return -(MathF.Cos(MathF.PI * x) - 1f) / 2f;
        }

        public static float EaseInQuad(float x)
        {
            return x * x;
        }
        public static float EaseOutQuad(float x)
        {
            return 1f - (1f - x) * (1f - x);
        }
        public static float EaseInOutQuad(float x)
        {
            return x < 0.5f ? 2f * x * x : 1f - MathF.Pow(-2f * x + 2f, 2f) / 2f;
        }
        public static float EaseInCubic(float x)
        {
            return x * x * x;
        }
        public static float EaseOutCubic(float x)
        {
            return 1f - MathF.Pow(1f - x, 3f);
        }
        public static float EaseInOutCubic(float x)
        {
            return x < 0.5f ? 4f * x * x * x : 1f - MathF.Pow(-2f * x + 2f, 3f) / 2f;
        }

        public static float EaseInQuart(float x)
        {
            return x * x * x * x;
        }
        public static float EaseOutQuart(float x)
        {
            return 1f - MathF.Pow(1f - x, 4f);
        }
        public static float EaseInOutQuart(float x)
        {
            return x < 0.5f ? 8f * x * x * x * x : 1f - MathF.Pow(-2f * x + 2f, 4f) / 2f;
        }

        public static float EaseInQuint(float x)
        {
            return x * x * x * x * x;
        }
        public static float EaseOutQuint(float x)
        {
            return 1 - MathF.Pow(1f - x, 5f);
        }
        public static float EaseInOutQuint(float x)
        {
            return x < 0.5f ? 16f * x * x * x * x * x : 1f - MathF.Pow(-2f * x + 2f, 5f) / 2f;
        }

        public static float EaseInExpo(float x)
        {
            return x == 0f ? 0f : MathF.Pow(2f, 10f * x - 10f);
        }
        public static float EaseOutExpo(float x)
        {
            return x == 1f ? 1f : 1f - MathF.Pow(2f, -10f * x);
        }
        public static float EaseInOutExpo(float x)
        {
            return x == 0f
            ? 0f
            : x == 1f
            ? 1f
            : x < 0.5f ? MathF.Pow(2f, 20f * x - 10f) / 2f
            : (2f - MathF.Pow(2f, -20f * x + 10f)) / 2f;
        }

        public static float EaseInCirc(float x)
        {
            return 1f - MathF.Sqrt(1 - MathF.Pow(x, 2f));
        }
        public static float EaseOutCirc(float x)
        {
            return MathF.Sqrt(1f - MathF.Pow(x - 1f, 2f));
        }
        public static float EaseInOutCirc(float x)
        {
            return x < 0.5f
            ? (1f - MathF.Sqrt(1f - MathF.Pow(2f * x, 2f))) / 2f
            : (MathF.Sqrt(1f - MathF.Pow(-2f * x + 2f, 2f)) + 1f) / 2f;
        }

        public static float EaseInBack(float x)
        {
            return c3 * x * x * x - c1 * x * x;
        }
        public static float EaseOutBack(float x)
        {
            return 1f + c3 * MathF.Pow(x - 1f, 3f) + c1 * MathF.Pow(x - 1f, 2f);
        }
        public static float EaseInOutBack(float x)
        {
            return x < 0.5f
            ? (MathF.Pow(2f * x, 2f) * ((c2 + 1f) * 2f * x - c2)) / 2f
            : (MathF.Pow(2f * x - 2f, 2f) * ((c2 + 1f) * (x * 2f - 2f) + c2) + 2f) / 2f;
        }

        public static float EaseInElastic(float x)
        {          
             return x == 0f
             ? 0f
             : x == 1f
             ? 1f
             : -MathF.Pow(2f, 10f * x - 10f) * MathF.Sin((x * 10f - 10.75f) * c4);
        }
        public static float EaseOutElastic(float x)
        {
            return x == 0f
            ? 0f
            : x == 1f
            ? 1f
            : MathF.Pow(2f, -10f * x) * MathF.Sin((x * 10f - 0.75f) * c4) + 1f;
        }
        public static float EaseInOutElastic(float x)
        {
            return x == 0f
              ? 0f
              : x == 1f
              ? 1f
              : x < 0.5f
              ? -(MathF.Pow(2f, 20f * x - 10f) * MathF.Sin((20f * x - 11.125f) * c5)) / 2f
              : (MathF.Pow(2f, -20f * x + 10f) * MathF.Sin((20f * x - 11.125f) * c5)) / 2f + 1f;
        }

        public static float EaseInBounce(float x)
        {
            return 1f - EaseOutBounce(1f - x);
        }
        public static float EaseOutBounce(float x)
        {
            if (x < 1f / d1)
            {
                return n1 * x * x;
            }
            else if (x < 2f / d1)
            {
                return n1 * (x -= 1.5f / d1) * x + 0.75f;
            }
            else if (x < 2.5f / d1)
            {
                return n1 * (x -= 2.25f / d1) * x + 0.9375f;
            }
            else
            {
                return n1 * (x -= 2.625f / d1) * x + 0.984375f;
            }
        }
        public static float EaseInOutBounce(float x)
        {
            return x < 0.5f
            ? (1f - EaseOutBounce(1f - 2f * x)) / 2f
            : (1f + EaseOutBounce(2f * x - 1f)) / 2f;
        }
    }
}