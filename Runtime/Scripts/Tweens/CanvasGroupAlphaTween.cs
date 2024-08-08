using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace HexTecGames.TweenLib
{
    public class CanvasGroupAlphaTween : Tween
    {
        private CanvasGroup CanvasGroup
        {
            get
            {
                return Data.target;
            }
        }

        private new CanvasGroupAlphaTweenData Data;
        private float startAlpha;

        public CanvasGroupAlphaTween(CanvasGroupAlphaTweenData data) : base(data)
        {
            Data = data;
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
            CanvasGroup.alpha = startAlpha;
        }

        protected override void DoAnimation(float time)
        {
            if (CanvasGroup == null)
            {
                return;
            }
            CanvasGroup.alpha = GetAnimationCurveValue(time);
        }

        public override void SetStartData()
        {
            startAlpha = CanvasGroup.alpha;
        }        
    }
    [System.Serializable]
    public class CanvasGroupAlphaTweenData : TweenData
    {
        [Space]
        public CanvasGroup target;
        public override Tween Create()
        {
            CanvasGroupAlphaTween tween = new CanvasGroupAlphaTween(this);
            return tween;
        }
        public override void GetRequiredComponent(GameObject go)
        {
            if (target != null)
            {
                return;
            }
            if (go.TryGetComponent(out CanvasGroup canvasGroup))
            {
                target = canvasGroup;
            }
            else target = go.AddComponent<CanvasGroup>();
        }
    }
}