using HexTecGames.Basics;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace HexTecGames.TweenLib
{
    [System.Serializable]
    public class TransformTween : VectorTween
    {              
        protected Transform targetTransform;

        private new TransformTweenData data;

        public TransformTween(TransformTweenData data) : base(data)
        {
            this.data = data;
        }
        public override void SetStartData()
        {
            startVector = GetVector();
        }

        private Vector3 GetVector()
        {
            switch (data.targetVector)
            {
                case TargetVector.Position:
                    if (data.space == Space.Local)
                    {
                        return targetTransform.localPosition;
                    }
                    return targetTransform.position;

                case TargetVector.Rotation:
                    if (data.space == Space.Local)
                    {
                        return targetTransform.localEulerAngles;
                    }
                    return targetTransform.eulerAngles;

                case TargetVector.Scale:
                    return targetTransform.localScale;

                default:
                    return Vector3.zero;
            }
        }

        private void ApplyVector(Vector3 vector)
        {
            if (targetTransform == null)
            {
                return;
            }
            switch (data.targetVector)
            {
                case TargetVector.Position:
                    if (data.space == Space.Local)
                    {
                        targetTransform.localPosition += vector;
                    }
                    else targetTransform.position += vector;
                    break;

                case TargetVector.Rotation:
                    if (data.space == Space.Local)
                    {
                        targetTransform.localEulerAngles += vector;
                    }
                    else targetTransform.eulerAngles += vector;
                    break;

                case TargetVector.Scale:
                    targetTransform.localScale += vector;
                    break;

                default:
                    break;
            }
        }
        protected override void DoAnimation(float time)
        {
            ApplyVector(CalculateVector(time, GetVector()));
        }

        protected override void SetStartObject(GameObject go)
        {
            targetTransform = go.transform;
        }
    }

    [System.Serializable]
    public class TransformTweenData : VectorData
    {
        [Space]
        public TargetVector targetVector;
        [DrawIf(nameof(targetVector), TargetVector.Scale, reverse: true)]
        public Space space = default;
        public override Tween Create()
        {
            TransformTween tween = new TransformTween(this);
            return tween;
        }
    }
}