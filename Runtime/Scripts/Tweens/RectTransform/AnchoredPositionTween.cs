using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace HexTecGames.TweenLib
{
    public class AnchoredPositionTween : RectTransformTween
    {
        public AnchoredPositionTween(AnchoredPositionTweenData data) : base(data)
        {
        }

        public override void SetStartData()
        {
            if (rectTransform == null)
            {
                return;
            }
            startVector = rectTransform.anchoredPosition;
        }

        protected override void DoAnimation(float time)
        {
            rectTransform.anchoredPosition = CalculateVector(time, rectTransform.anchoredPosition);
        }

        protected override void SetStartObject(GameObject go)
        {
            if (go != null)
            {
                rectTransform = go.GetComponent<RectTransform>();
            }
        }
    }
    [System.Serializable]
    public class AnchoredPositionTweenData : VectorData
    {
        public override Tween Create()
        {
            AnchoredPositionTween tween = new AnchoredPositionTween(this);
            return tween;
        }
    }
}