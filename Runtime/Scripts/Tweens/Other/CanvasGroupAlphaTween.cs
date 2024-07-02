using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace HexTecGames.TweenLib
{
    public class CanvasGroupAlphaTween : Tween
    {
        private CanvasGroup canvasGroup;

        //private new CanvasGroupAlphaTweenData Data;
        private float startAlpha;

        public CanvasGroupAlphaTween(CanvasGroupAlphaTweenData data) : base(data)
        {
            //Data = data;
        }

        protected override void SetStartObject(GameObject go)
        {
            canvasGroup = go.GetComponent<CanvasGroup>();
            if (canvasGroup == null)
            {
                Debug.Log($"{go.name} does not have a {typeof(CanvasGroup)} component");
            }
        }

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
        public override Tween Create()
        {
            CanvasGroupAlphaTween tween = new CanvasGroupAlphaTween(this);
            return tween;
        }
        public override void AddRequiredComponents(GameObject go)
        {
            if (go.GetComponent<CanvasGroup>() != null)
            {
                return;
            }
            go.AddComponent<CanvasGroup>();
        }
    }
}