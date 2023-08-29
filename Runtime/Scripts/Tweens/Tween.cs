using HexTecGames.Basics;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace HexTecGames.TweenLib
{
    [System.Serializable]
    public abstract class Tween
    {
        public AnimationCurve animationCurve = new AnimationCurve(new Keyframe[] { new Keyframe(0, 0), new Keyframe(1, 0) });
        public float Length
        {
            get
            {
                return animationCurve.keys[animationCurve.length - 1].time;
            }
        }

        public bool IsFinished
        {
            get
            {
                return isFinished;
            }
            set
            {
                isFinished = value;
            }
        }
        private bool isFinished = false;

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

        public bool Loop
        {
            get
            {
                return loop;
            }
            set
            {
                loop = value;
            }
        }
        [SerializeField] private bool loop = false;
        public int Repeats
        {
            get
            {
                return repeats;
            }
            set
            {
                repeats = value;
            }
        }
        [DrawIf("loop", false)]
        [SerializeField] private int repeats = default;


        public float Delay
        {
            get
            {
                return delay;
            }
            set
            {
                delay = value;
            }
        }
        [SerializeField] private float delay = 0;

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
        [DrawIf("delay", 0f, reverse: true)]
        [SerializeField] private bool applyImmediately = true;


        public float Speed
        {
            get
            {
                return speed;
            }
            set
            {
                speed = value;
            }
        }
        [Min(0.001f)][SerializeField] private float speed = 1;

        public bool Reverse
        {
            get
            {
                return reverse;
            }
            set
            {
                reverse = value;
            }
        }
        private bool reverse = default;

        public float Duration
        {
            get
            {
                return (Length / Speed) * (Repeats + 1) + Delay;
            }
        }

        public float AnimationTime
        {
            get
            {
                return (elapsedTime - Delay) * Speed;
            }
        }

        private float elapsedTime;

        protected GameObject targetGO;

        public void SetStartValues()
        {
            Init(targetGO);
        }
        public virtual void Init(GameObject go)
        {
            targetGO = go;
            SetStartData();
        }
        protected Tween(TweenData data)
        {
            this.animationCurve = data.AnimationCurve;
            Loop = data.Loop;
            Repeats = data.Repeats;
            Delay = data.Delay;
            Speed = data.Speed;
        }

        public Tween()
        {
        }


        //protected abstract void ApplyData(TweenData data);
        //public void LoadData(TweenData data)
        //{
        //    ApplyData(data);
        //    animationCurve = data.AnimationCurve;
        //    Loop = data.Loop;
        //    Delay = data.Delay;
        //    Speed = data.Speed;
        //}

        protected abstract TweenData CreateData();
        public virtual TweenData GetData()
        {
            TweenData data = CreateData();
            //if (Mathf.Approximately(1.11f, data.Speed))
            //{
            //    Debug.Log(data.AnimationCurve.keys[^1].value);
            //}
            data.AnimationCurve = animationCurve;
            data.Loop = Loop;
            data.Repeats = Repeats;
            data.Delay = Delay;
            data.Speed = Speed;
            return data;
        }

        public void Start()
        {
            IsFinished = false;
            if (applyImmediately || Delay <= 0)
            {
                if (Reverse)
                {
                    DoAnimation(Length);
                }
                else DoAnimation(0);
            }
        }
        public void Stop()
        {
            IsFinished = true;
            if (Reverse)
            {
                DoAnimation(0);
            }
            else DoAnimation(Length);
        }

        protected float EvaluateCurve(float time)
        {
            return animationCurve.Evaluate(time);
        }

        public void Evaluate(float elapsedTime)
        {
            this.elapsedTime = elapsedTime;
            if (IsFinished || IsEnabled == false)
            {
                return;
            }
            if (Speed <= 0)
            {
                return;
            }
            if (Delay > elapsedTime)
            {
                return;
            }
            if (Loop == false && AnimationTime >= Length * (Repeats + 1))
            {
                Stop();
                return;
            }
            else
            {
                if (Reverse)
                {
                    DoAnimation(Length - AnimationTime % Length);
                }
                else DoAnimation(AnimationTime % Length);
            }
        }
        protected abstract void DoAnimation(float time);

        protected abstract void SetStartData();
    }
    [System.Serializable]
    public abstract class TweenData
    {
        public AnimationCurve AnimationCurve;
        public bool Loop;
        public int Repeats;
        public float Delay;
        public float Speed;

        public abstract Tween Create();
    }
}