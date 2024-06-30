//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;

//namespace HexTecGames.TweenLib
//{

//    public class PositionTween : TransformTween
//    {
//        public PositionTween(PositionTweenData data) : base(data)
//        { 

//        }
       
//        protected override void DoAnimation(float time)
//        {
//            PositionTweenData posData = Data as PositionTweenData;
//            if (posData.space == Space.Local)
//            {
//                targetTransform.localPosition = CalculateVector(time, targetTransform.localPosition);
//            }
//            else targetTransform.position = CalculateVector(time, targetTransform.position);

//        }
//        public override void SetStartData()
//        {
//            if (targetTransform == null)
//            {
//                return;
//            }
//            PositionTweenData posData = Data as PositionTweenData;
//            if (posData.space == Space.Local)
//            {
//                startVec = targetTransform.localPosition;
//            }
//            else startVec = targetTransform.position;
//        }
//    }
//    [System.Serializable]
//    public class PositionTweenData : VectorData
//    {
//        [SerializeField] public Space space = default;
//        public override Tween Create()
//        {
//            PositionTween tween = new PositionTween(this);
//            return tween;
//        }
//    }
//}