using System;
using UnityEngine;

namespace HexTecGames.TweenLib
{
    public class CanvasGroupAlphaTween : Tween
    {
        private CanvasGroup canvasGroup;

        private new CanvasGroupAlphaTweenData Data;
        private float startAlpha;

        public CanvasGroupAlphaTween(CanvasGroupAlphaTweenData data, CanvasGroup canvasGroup) : base(data, canvasGroup)
        {
            Data = data;
            this.canvasGroup = canvasGroup;
        }

        //protected override void SetStartObject(Component component)
        //{
        //    if (component is CanvasGroup canvasGroup)
        //    {
        //        this.canvasGroup = canvasGroup;
        //    }
        //}
        //protected override void SetStartObject(GameObject go)
        //{
        //    canvasGroup = go.GetComponent<CanvasGroup>();
        //    if (canvasGroup == null)
        //    {
        //        Debug.Log($"{go.name} does not have a {typeof(CanvasGroup)} component");
        //    }
        //}

        public override void ResetEffect()
        {
            canvasGroup.alpha = startAlpha;
        }

        protected override void DoAnimation(float time)
        {
            if (canvasGroup == null)
            {
                return;
            }
            canvasGroup.alpha = GetAnimationCurveValue(time);
        }

        public override void SetStartData()
        {
            startAlpha = canvasGroup.alpha;
        }
    }
    [System.Serializable]
    public class CanvasGroupAlphaTweenData : TweenData
    {
        public override Tween Create(Component component)
        {
            CanvasGroupAlphaTween tween = new CanvasGroupAlphaTween(this, component as CanvasGroup);
            return tween;
        }

        protected override Type GetTargetType()
        {
            return typeof(CanvasGroup);
        }
    }
}