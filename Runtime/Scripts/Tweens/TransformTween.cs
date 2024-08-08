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
        //protected Transform targetTransform;

        private Transform targetTransform
        {
            get
            {
                return data.target;
            }
        }

        private new TransformTweenData data;

        public TransformTween(TransformTweenData data) : base(data)
        {
            this.data = data;
        }
        public override void SetStartData()
        {
            startVector = GetVector();
        }
        public override void ResetEffect()
        {
            SetVector(startVector);
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

        private void SetVector(Vector3 vector)
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
                        targetTransform.localPosition = vector;
                    }
                    else targetTransform.position = vector;
                    break;

                case TargetVector.Rotation:
                    if (data.space == Space.Local)
                    {
                        targetTransform.localEulerAngles = vector;
                    }
                    else targetTransform.eulerAngles = vector;
                    break;

                case TargetVector.Scale:
                    targetTransform.localScale = vector;
                    break;

                default:
                    break;
            }
        }
        //private void AddVector(Vector3 vector)
        //{
        //    if (targetTransform == null)
        //    {
        //        return;
        //    }
        //    switch (data.targetVector)
        //    {
        //        case TargetVector.Position:
        //            if (data.space == Space.Local)
        //            {
        //                targetTransform.localPosition += vector;
        //            }
        //            else targetTransform.position += vector;
        //            break;

        //        case TargetVector.Rotation:
        //            if (data.space == Space.Local)
        //            {
        //                targetTransform.localEulerAngles += vector;
        //            }
        //            else targetTransform.eulerAngles += vector;
        //            break;

        //        case TargetVector.Scale:
        //            targetTransform.localScale += vector;
        //            break;

        //        default:
        //            break;
        //    }
        //}
        protected override void DoAnimation(float time)
        {
            SetVector(CalculateVector(time, GetVector()));
        }

        //protected override void SetStartObject(GameObject go)
        //{
        //    targetTransform = go.transform;
        //}
        //protected override void SetStartObject(Component component)
        //{
        //    if (component is Transform transform)
        //    {
        //        targetTransform = transform;
        //    }
        //}
    }

    [System.Serializable]
    public class TransformTweenData : VectorData
    {
        [Space]
        public Transform target;
        public TargetVector targetVector;
        [DrawIf(nameof(targetVector), TargetVector.Scale, reverse: true)]
        public Space space = default;
        public override Tween Create()
        {
            TransformTween tween = new TransformTween(this);
            return tween;
        }

        public override void GetRequiredComponent(GameObject go)
        {
            target = go.transform;
        }
    }
}