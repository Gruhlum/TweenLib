//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;
//using Random = UnityEngine.Random;

//namespace HexTecGames.TweenLib
//{
//    [System.Serializable]
//    public class RotationTween : Tween
//    {
//        private Quaternion startRotation;
//        private Transform targetT;

//        private RotationTweenData rotationData;

//        public RotationTween(RotationTweenData data) : base(data)
//        {
//            rotationData = data;
//        }

//        public override void ResetEffect()
//        {
//            targetT.transform.rotation = startRotation;
//        }

//        public override void SetStartData()
//        {
//            if (rotationData.space == Space.Local)
//            {
//                startRotation = targetT.localRotation;
//            }
//            else startRotation = targetT.rotation;
//        }

//        protected override void DoAnimation(float time)
//        {           
//            targetT.Rotate(rotationData.rotationVector * GetAnimationCurveValue(time));
//        }

//        protected override void SetStartObject(GameObject go)
//        {
//            targetT = go.GetComponent<Transform>();
//        }

//        protected override void SetStartObject(Component component)
//        {
//            targetT = component as Transform;
//        }
//    }
//    [System.Serializable]
//    public class RotationTweenData : TweenData
//    {
//        public Vector3 rotationVector;
//        public Space space;

//        public override Tween Create()
//        {
//            RotationTween tween = new RotationTween(this);
//            return tween;
//        }       
//    }
//}