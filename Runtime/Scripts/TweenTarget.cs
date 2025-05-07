using HexTecGames.TweenLib;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace HexTecGames.TweenLib
{
    [System.Serializable]
    public class TweenTarget
    {
        [SerializeField][SubclassSelector, SerializeReference] private TweenData data;
        [SerializeField][Space] private Component target;

        public bool IsEndless
        {
            get
            {
                return data.EndlessLoop;
            }
        }

        public TweenTarget()
        { }
        public TweenTarget(GameObject go) : this()
        {
            FindCorrectTarget(go);
        }
        public TweenTarget(GameObject go, TweenData data) : this(go)
        {
            this.data = data;
        }
        public void FindCorrectTarget(GameObject go)
        {
            if (data == null)
            {
                return;
            }
            if (target != null)
            {
                return;
            }
            target = data.FindCorrectComponent(go);
        }
        public void PerformTargetCheck()
        {
            if (data == null)
            {
                return;
            }
            if (target == null)
            {
                return;
            }
            if (!data.CheckForCorrectComponent(target))
            {
                target = data.FindCorrectComponent(target.gameObject);
                //Debug.Log($"Wrong Type: Component must be of type '{data.GetTargetType()}'");
            }
        }
        public Tween Create()
        {
            return data.Create(target);
        }
    }
}