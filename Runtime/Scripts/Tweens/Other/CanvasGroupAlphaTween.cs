using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace HexTecGames.TweenLib
{
    [System.Serializable]
    public class CanvasGroupAlphaTween : Tween
    {
        private CanvasGroup canvasGroup;
        public override void Init(GameObject go)
        {
            canvasGroup = go.GetComponent<CanvasGroup>();
        }

        protected override TweenData CreateData()
        {
            //TODO
            throw new System.NotImplementedException();
        }

        protected override void DoAnimation(float time)
        {
            canvasGroup.alpha = EvaluateCurve(time);
        }
    }
}