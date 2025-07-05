using System;
using HexTecGames.Basics;
using HexTecGames.EaseFunctions;
using UnityEngine;

namespace HexTecGames.TweenLib
{
    [System.Serializable]
    public abstract class TweenData
    {
        public bool customCurve;
        [DrawIf(nameof(customCurve), false)] public EasingType easing;
        [DrawIf(nameof(customCurve), false)] public FunctionType function;
        [DrawIf(nameof(customCurve), true)]
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
        public bool EndlessLoop
        {
            get
            {
                return this.endlessLoop;
            }
            private set
            {
                this.endlessLoop = value;
            }
        }
        public float LoopWaitTime
        {
            get
            {
                return this.loopWaitTime;
            }

            set
            {
                this.loopWaitTime = value;
            }
        }
        public LoopMode LoopMode
        {
            get
            {
                return this.loopMode;
            }

            set
            {
                this.loopMode = value;
            }
        }
        public int Repeats
        {
            get
            {
                return this.repeats;
            }

            set
            {
                this.repeats = value;
            }
        }
        public float StartDelay
        {
            get
            {
                return this.startDelay;
            }

            set
            {
                this.startDelay = value;
            }
        }
        public float EndDelay
        {
            get
            {
                return this.endDelay;
            }

            set
            {
                this.endDelay = value;
            }
        }
        public float Speed
        {
            get
            {
                return this.speed;
            }

            set
            {
                this.speed = value;
            }
        }

        [Space][SerializeField] private bool reverse = default;

        [SerializeField] private bool endlessLoop;
        [SerializeField] private LoopMode loopMode;
        [SerializeField] private float loopWaitTime = default;
        [DrawIf(nameof(endlessLoop), false)]
        [SerializeField] private int repeats;
        [Space]
        [SerializeField, Min(0.001f)] private float speed = 1;
        [SerializeField] private float startDelay;
        [SerializeField] private float endDelay;

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

        public TweenData CreateShallowCopy()
        {
            return MemberwiseClone() as TweenData;
        }

        protected abstract Type GetTargetType();
    }
}