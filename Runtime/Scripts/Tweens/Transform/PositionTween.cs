using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace HexTecGames.TweenLib
{
    [System.Serializable]
    public class PositionTween : TransformTween
    {
        public PositionTween() { }
        public PositionTween(PositionData data) : base(data)
        {         
        }
       
        protected override void DoAnimation(float time)
        {
            targetTransform.localPosition = CalculateVector(time);
        }
        protected override void SetStartData()
        {
            startVec = targetTransform.transform.localPosition;
        }

        protected override TweenData CreateData()
        {
            PositionData data = new PositionData();
            return data;
        }
    }
    [System.Serializable]
    public class PositionData : TransformData
    {
        public override Tween Create()
        {
            return new PositionTween(this);
        }
    }
}