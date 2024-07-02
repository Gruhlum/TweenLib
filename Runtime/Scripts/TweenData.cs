using HexTecGames.Basics;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace HexTecGames.TweenLib
{
    [System.Serializable]
    public abstract class TweenData
    {
        public bool CustomCurve;
        [DrawIf(nameof(CustomCurve), false)] public AnimationType animationType;
        [DrawIf(nameof(CustomCurve), false)] public Curve curve;
        [DrawIf(nameof(CustomCurve), true)] public AnimationCurve animationCurve 
            = new AnimationCurve(new Keyframe[] { new Keyframe(0, 0), new Keyframe(1, 1) });

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
        //public bool ApplyImmediately
        //{
        //    get
        //    {
        //        return applyImmediately;
        //    }
        //    set
        //    {
        //        applyImmediately = value;
        //    }
        //}
        //[DrawIf(nameof(StartDelay), 0f, reverse: true)][SerializeField] private bool applyImmediately = true;
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
       

        public abstract Tween Create();
        public virtual void AddRequiredComponents(GameObject go) { }
    }
}