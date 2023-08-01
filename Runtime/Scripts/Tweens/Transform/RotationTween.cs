using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace HexTecGames.TweenLib
{
    [System.Serializable]
    public class RotationTween : TransformTween
    {
        public RotationTween() { }
        public RotationTween(RotationTweenData data) : base(data)
        {
        }

        public override void Init(GameObject go)
        {
            base.Init(go);
            
        }
        protected override void SetStartData()
        {
            startVec = targetTransform.localEulerAngles;
        }

        protected override void DoAnimation(float time)
        {
            Vector3 vec = CalculateVector(time);

            //Debug.Log(targetTransform.localEulerAngles + " - " + vec);
            targetTransform.localEulerAngles = vec;
        }

        protected override TweenData CreateData()
        {
            RotationTweenData data = new RotationTweenData();
            return data;
        }
    }
    public class RotationTweenData : TransformData
    {
        public override Tween Create()
        {
            return new RotationTween(this);
        }
    }
}