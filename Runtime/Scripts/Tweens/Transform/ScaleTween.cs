using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace HexTecGames.TweenLib
{
    public class ScaleTween : TransformTween
    {
        public ScaleTween() { }


        public override void SetStartData()
        {
            startVec = targetTransform.localScale;
        }
        protected override void DoAnimation(float time)
        {
            if (targetTransform == null)
            {
                return;
            }
            targetTransform.localScale = CalculateVector(time);
        }
    }
    [System.Serializable]
    public class ScaleTweenData : TransformData
    {
        public override Tween Create()
        {
            ScaleTween tween = new ScaleTween();
            tween.Data = this;
            return tween;
        }
    }
}