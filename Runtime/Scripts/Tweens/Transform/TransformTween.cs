using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace HexTecGames.TweenLib
{
    [System.Serializable]
    public abstract class TransformTween : Tween
    {
        protected Transform transform;

        public TransformTween() { }
        protected TransformTween(TweenData data) : base(data)
        {
        }

        public override void Init(GameObject go)
        {
            transform = go.transform;
        }
    }
}