using HexTecGames.Basics;
using HexTecGames.EaseFunctions;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace HexTecGames.TweenLib
{
    [System.Serializable]
    public abstract class TweenData
    {
        public bool CustomCurve;
        [DrawIf(nameof(CustomCurve), false)] public Easing easing;
        [DrawIf(nameof(CustomCurve), false)] public Function function;
        [DrawIf(nameof(CustomCurve), true)]
        public AnimationCurve animationCurve = new AnimationCurve(new Keyframe[] { new Keyframe(0, 0), new Keyframe(1, 1) });

        public bool Reverse
        {
            get
            {
                return reverse;
            }
            private set
            {
                reverse = value;
            }
        }
        [Space][SerializeField] private bool reverse = default;
        [Space] public bool EndlessLoop;
        public LoopMode LoopMode;
        [DrawIf(nameof(EndlessLoop), false)]
        public int Repeats;

        [Space, Min(0.001f)] public float Speed = 1;
        public float StartDelay;
        public float EndDelay;

        //public bool SetStartDataBeforePlay
        //{
        //    get
        //    {
        //        return setStartDataBeforePlay;
        //    }
        //    private set
        //    {
        //        setStartDataBeforePlay = value;
        //    }
        //}
        //[SerializeField] private bool setStartDataBeforePlay;


        public abstract Tween Create(Component component);
        public virtual bool CheckForCorrectComponent(Component component)
        {
            if (GetTargetType().IsAssignableFrom(component.GetType()))
            {
                return true;
            }
            else return false;
        }
        public virtual Component FindCorrectComponent(GameObject go)
        {
            return go.GetComponent(GetTargetType());
        }

        protected abstract Type GetTargetType();
    }
}