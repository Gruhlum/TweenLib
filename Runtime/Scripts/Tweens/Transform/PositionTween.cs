using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace HexTecGames.TweenLib
{
    public class PositionTween : TransformTween
    {
        public PositionTween() { }
       
        protected override void DoAnimation(float time)
        {
            targetTransform.localPosition = CalculateVector(time);
        }
        protected override void SetStartData()
        {
            startVec = targetTransform.transform.localPosition;
        }
    }
    [System.Serializable]
    public class PositionTweenData : TransformData
    {
        public override Tween Create()
        {
            PositionTween tween = new PositionTween();
            tween.Data = this;
            return tween;
        }
    }
}