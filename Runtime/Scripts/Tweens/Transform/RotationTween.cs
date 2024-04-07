using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace HexTecGames.TweenLib
{
    public class RotationTween : TransformTween
    {
        public RotationTween(RotationTweenData data) : base(data)
        { }

        public override void SetStartData()
        {
            startVec = targetTransform.eulerAngles;
        }

        protected override void DoAnimation(float time)
        {
            if (targetTransform == null)
            {
                return;
            }
            RotationTweenData rotationData = Data as RotationTweenData;
            if (rotationData.space == Space.Local)
            {
                targetTransform.localEulerAngles = CalculateVector(time, targetTransform.localEulerAngles);
            }
            else targetTransform.eulerAngles = CalculateVector(time, targetTransform.eulerAngles);
        }
    }
    [System.Serializable]
    public class RotationTweenData : TransformData
    {
        [SerializeField] public Space space = default;
        public override Tween Create()
        {
            RotationTween tween = new RotationTween(this);
            return tween;
        }
    }
}