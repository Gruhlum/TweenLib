using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace HexTecGames.TweenLib
{
    public class RotationTween : TransformTween
    {
        public RotationTween() { }

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
            Vector3 vec = CalculateVector(time, targetTransform.eulerAngles);
            targetTransform.eulerAngles = vec;
        }
    }
    [System.Serializable]
    public class RotationTweenData : TransformData
    {
        public override Tween Create()
        {
            RotationTween tween = new RotationTween();
            tween.Data = this;
            return tween;
        }
    }
}