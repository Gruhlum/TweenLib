using HexTecGames.Basics;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace HexTecGames.TweenLib
{
    [System.Serializable]
    public abstract class TweenData
    {
        public AnimationCurve animationCurve = new AnimationCurve(new Keyframe[] { new Keyframe(0, 0), new Keyframe(1, 1) });
        public bool Loop;
        [DrawIf(nameof(Loop), false)]
        public int Repeats;
        [Min(0.001f)]
        public float Speed = 1;
        public float Delay;
        public bool ApplyImmediately
        {
            get
            {
                return applyImmediately;
            }
            set
            {
                applyImmediately = value;
            }
        }
        [DrawIf(nameof(Delay), 0f, reverse: true)][SerializeField] private bool applyImmediately = true;
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
        //[DrawIf(nameof(ShowSetStartData), false)][SerializeField] private bool setStartDataBeforePlay = true;        
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
        public bool IsEnabled
        {
            get
            {
                return isEnabled;
            }
            set
            {
                isEnabled = value;
            }
        }
        [SerializeField] private bool isEnabled = true;


        public abstract Tween Create();
        public virtual void AddRequiredComponents(GameObject go) { }
    }
}