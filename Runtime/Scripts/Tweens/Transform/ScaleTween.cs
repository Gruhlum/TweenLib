//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;

//namespace HexTecGames.TweenLib
//{
//    public class ScaleTween : TransformTween
//    {
//        public ScaleTween(ScaleTweenData data) : base(data)
//        { }

//        public override void SetStartData()
//        {
//            startVec = targetTransform.localScale;
//        }
//        protected override void DoAnimation(float time)
//        {
//            if (targetTransform == null)
//            {
//                return;
//            }
//            targetTransform.localScale = CalculateVector(time, targetTransform.localScale);
//        }
//    }
//    [System.Serializable]
//    public class ScaleTweenData : VectorData
//    {
//        public override Tween Create()
//        {
//            ScaleTween tween = new ScaleTween(this);
//            return tween;
//        }
//    }
//}