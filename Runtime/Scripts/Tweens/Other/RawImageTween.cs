using HexTecGames.Basics;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace HexTecGames.TweenLib
{
    [System.Serializable]
    public class RawImageTween : Tween
    {
        private RawImage targetImage;
        private RawImageTweenData rawImageData;

        private Rect startRect;

        public RawImageTween(RawImageTweenData rawImageData, RawImage target) : base(rawImageData, target)
        {
            this.rawImageData = rawImageData;
            this.targetImage = target;
        }

        public override void ResetEffect()
        {
            targetImage.uvRect = startRect;
        }

        public override void SetStartData()
        {
            startRect = targetImage.uvRect;
        }

        protected override void DoAnimation(float time)
        {
            Rect targetRect = targetImage.uvRect;
            if (rawImageData.positionX)
            {
                targetRect.x = GetAnimationCurveValue(time) * rawImageData.multiplierPositionX * rawImageData.strength;
            }
            if (rawImageData.positionY)
            {
                targetRect.y = GetAnimationCurveValue(time) * rawImageData.multiplierPositionY * rawImageData.strength;
            }
            if (rawImageData.sizeX)
            {
                targetRect.width = GetAnimationCurveValue(time) * rawImageData.multiplierSizeX * rawImageData.strength;
            }
            if (rawImageData.sizeY)
            {
                targetRect.height = GetAnimationCurveValue(time) * rawImageData.multiplierSizeY * rawImageData.strength;
            }
            targetImage.uvRect = targetRect;
        }
    }

    [System.Serializable]
    public class RawImageTweenData : TweenData
    {
        public float strength = 1;

        [Space]
        public bool positionX;
        [DrawIf(nameof(positionX), true)] public float multiplierPositionX = 1;
        public bool positionY;
        [DrawIf(nameof(positionY), true)] public float multiplierPositionY = 1;
        public bool sizeX;
        [DrawIf(nameof(sizeX), true)] public float multiplierSizeX = 1;
        public bool sizeY;
        [DrawIf(nameof(sizeY), true)] public float multiplierSizeY = 1;

        public override Tween Create(Component component)
        {
            RawImageTween tween = new RawImageTween(this, component as RawImage);
            return tween;
        }

        protected override Type GetTargetType()
        {
            return typeof(RawImage);
        }
    }
}