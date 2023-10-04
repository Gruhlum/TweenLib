using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace HexTecGames.TweenLib
{
    public class CanvasGroupAlphaTween : Tween
    {
        private CanvasGroup canvasGroup;

        public CanvasGroupAlphaTween() { }

        protected override void SetStartObject(GameObject go)
        {
            canvasGroup = go.GetComponent<CanvasGroup>();
        }

        protected override void DoAnimation(float time)
        {
            if (canvasGroup == null)
            {
                return;
            }
            canvasGroup.alpha = EvaluateCurve(time);
        }

        public override void SetStartData()
        {
        }        
    }
    [System.Serializable]
    public class CanvasGroupAlphaTweenData : TweenData
    {
        public override Tween Create()
        {
            CanvasGroupAlphaTween tween = new CanvasGroupAlphaTween();
            tween.Data = this;
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