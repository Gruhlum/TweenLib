using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace HexTecGames.TweenLib
{
    [System.Serializable]
    public class ScaleTween : TransformTween
    {
        public ScaleTween() { }
        public ScaleTween(ScaleTweenData data) : base(data)
        {
        }

        public override void Init(GameObject go)
        {
            base.Init(go);           
        }
        protected override void SetStartData()
        {
            startVec = targetTransform.localScale;
        }
        protected override void DoAnimation(float time)
        {
            targetTransform.localScale = CalculateVector(time);
        }

        protected override TweenData CreateData()
        {
            ScaleTweenData data = new ScaleTweenData();
            return data;
        }
    }
    public class ScaleTweenData : TransformData
    {
        public override Tween Create()
        {
            return new ScaleTween(this);
        }
    }
}