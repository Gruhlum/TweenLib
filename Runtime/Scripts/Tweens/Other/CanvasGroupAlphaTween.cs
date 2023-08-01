using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace HexTecGames.TweenLib
{
    [System.Serializable]
    public class CanvasGroupAlphaTween : Tween
    {
        private CanvasGroup canvasGroup;

        public CanvasGroupAlphaTween() { }
        public CanvasGroupAlphaTween(CanvasGroupAlphaTweenData data) : base(data)
        {
        }

        public override void Init(GameObject go)
        {
            canvasGroup = go.GetComponent<CanvasGroup>();
        }

        protected override TweenData CreateData()
        {
            CanvasGroupAlphaTweenData data = new CanvasGroupAlphaTweenData();
            return data;
        }

        protected override void DoAnimation(float time)
        {
            canvasGroup.alpha = EvaluateCurve(time);           
        }

        protected override void SetStartData()
        {

        }
    }
    public class CanvasGroupAlphaTweenData : TweenData
    {
        public int bogus;
        public override Tween Create()
        {
            return new CanvasGroupAlphaTween(this);
        }
    }
}