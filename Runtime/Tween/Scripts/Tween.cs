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
        [SerializeField] private bool isEnabled = false;
      
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
        [SerializeField] private bool loop = true;

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

        public abstract void Init(GameObject go);

        public Tween()
        {
        }
        protected Tween(TweenData data)
        {
            this.animationCurve = data.AnimationCurve;
            Loop = data.Loop;
            Delay = data.Delay;
            Speed = data.Speed;
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
            data.AnimationCurve = animationCurve;
            data.Loop = Loop;
            data.Speed = Speed;
            return data;
        }

        protected float EvaluateCurve(float time)
        {
            return animationCurve.Evaluate(time);
        }

        public event Action<Tween> FinishedPlaying; 

        public void Evaluate(float elapsedTime)
        {
            if (IsFinished || IsEnabled == false)
            {
                return;
            }
            if (Speed == 0)
            {
                return;
            }
            if (Delay > elapsedTime)
            {
                return;
            }
            if (Loop == false && elapsedTime - Delay > Length)
            {
                IsFinished = true;
                FinishedPlaying?.Invoke(this);
                return;
            }
            else DoAnimation((elapsedTime - Delay) * Speed % Length);
        }
        protected abstract void DoAnimation(float time);
    }
    [System.Serializable]
    public abstract class TweenData
    {
        public AnimationCurve AnimationCurve;
        public bool Loop;
        public float Delay;
        public float Speed;

        public abstract Tween Create();
    }
}