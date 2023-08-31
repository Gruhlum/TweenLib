using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace HexTecGames.TweenLib
{
    public class RotationTween : TransformTween
    {
        public RotationTween() { }

        public override void Init(GameObject go)
        {
            base.Init(go);
            
        }
        protected override void SetStartData()
        {
            startVec = targetTransform.eulerAngles;
        }

        protected override void DoAnimation(float time)
        {
            Vector3 vec = CalculateVector(time);

            //Debug.Log(targetTransform.localEulerAngles + " - " + vec);
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