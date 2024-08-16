using HexTecGames.Basics;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace HexTecGames.TweenLib
{
    public class RectTransformTween : Tween
    {
        protected RectTransform rectTransform;

        private Vector2 startSizeDelta;

        private RectTransformTweenData data;

        public RectTransformTween(RectTransformTweenData data, RectTransform target) : base(data, target)
        {
            this.data = data;
            rectTransform = target;
        }

        public override void ResetEffect()
        {
            rectTransform.sizeDelta = startSizeDelta;
        }

        public override void SetStartData()
        {
            startSizeDelta = rectTransform.sizeDelta;
        }

        protected override void DoAnimation(float time)
        {
            Vector2 targetSizeDelta = startSizeDelta;
            if (data.width)
            {
                targetSizeDelta.x = CalculateResult(targetSizeDelta.x, GetAnimationCurveValue(time) * data.widthMultiplier * data.strength);
            }
            else targetSizeDelta.x = rectTransform.sizeDelta.x;
            if (data.height)
            {
                targetSizeDelta.y = CalculateResult(targetSizeDelta.y, GetAnimationCurveValue(time) * data.heightMultiplier * data.strength);
            }
            else targetSizeDelta.y = rectTransform.sizeDelta.y;

            rectTransform.sizeDelta = targetSizeDelta;
        }

        private float CalculateResult(float number1, float number2)
        {
            switch (data.mode)
            {
                case Mode.Addition:
                    return number1 + number2;
                case Mode.Multiply:
                    return number1 + number1 * number2;
                case Mode.Set:
                    return number2;
                default:
                    return 0;
            }
        }
    }

    [System.Serializable]
    public class RectTransformTweenData : TweenData
    {
        [Space]
        public float strength = 1f;
        public Mode mode;
        public bool width;
        [DrawIf(nameof(width), true)] public float widthMultiplier = 1f;
        public bool height;
        [DrawIf(nameof(height), true)] public float heightMultiplier = 1f;

        public override Tween Create(Component component)
        {
            return new RectTransformTween(this, component as RectTransform);
        }

        protected override Type GetTargetType()
        {
            return typeof(RectTransform);
        }

        //public override void CheckForCorrectComponent(GameObject go)
        //{
        //    if (target == null)
        //    {
        //        target = go.GetComponent<RectTransform>();
        //    }
        //}
    }
}