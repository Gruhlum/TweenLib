using HexTecGames.Basics;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace HexTecGames.TweenLib
{
    public abstract class Tween
    {
        public float Length
        {
            get
            {
                return AnimationLength / Data.Speed * (Data.Repeats + 1);
            }
        }

        public float AnimationLength
        {
            get
            {
                return animationLength;
            }
            private set
            {
                animationLength = value;
            }
        }
        [SerializeField] private float animationLength;


        //public bool IsFinished
        //{
        //    get
        //    {
        //        return isFinished;
        //    }
        //    set
        //    {
        //        isFinished = value;
        //    }
        //}
        //private bool isFinished = false;

        public float Duration
        {
            get
            {
                return (Length / data.Speed) + Delay;
            }
        }

        public float Delay
        {
            get
            {
                return delay;
            }
            private set
            {
                delay = value;
            }
        }
        private float delay;

        public float AnimationTime
        {
            get
            {
                return (elapsedTime - Delay) * data.Speed;
            }
        }

        private float elapsedTime;

        protected GameObject targetGO;

        public TweenData Data
        {
            get
            {
                return data;
            }
            set
            {
                data = value;
            }
        }
        [SerializeField] private TweenData data = default;

        //private bool temporaryReverse;
        //private bool startDataIsSet;

        //public bool Reversed
        //{
        //    get
        //    {
        //        if (temporaryReverse)
        //        {
        //            return !Data.Reverse;
        //        }
        //        else return Data.Reverse;
        //    }
        //}

        private bool reversed;

        private Func<float, float> animationCurve;

        public Tween(TweenData data)
        {
            this.Data = data;
            Delay = data.Delay;
        }

        public void Init(GameObject go)
        {
            targetGO = go;
            SetStartObject(go);
            SetStartData();
            
            UpdateAnimationCurve();
        }
        public void UpdateAnimationCurve()
        {
            if (!data.CustomCurve)
            {
                animationCurve = AnimationData.GetFunction(data.animationType, data.curve);
                AnimationLength = 1;
            }
            else AnimationLength = data.animationCurve.keys[^0].time;

        }
        public void AddDelay(float time)
        {
            Delay += time;
        }
        protected abstract void SetStartObject(GameObject go);

        public void Start(bool reversed = false)
        {
            if (Data == null)
            {
                return;
            }

            this.reversed = reversed;
            if (data.ApplyImmediately || Delay <= 0)
            {
                if (reversed)
                {
                    DoAnimation(Length);
                }
                else DoAnimation(0);
            }
        }

        public void Stop()
        {
            if (reversed)
            {
                DoAnimation(0);
            }
            else DoAnimation(Length);
        }
        
        public virtual void ResetEffect()
        {
            DoAnimation(0);
            // Not neccessary for some reason.
            //if (Reversed)
            //{
            //    DoAnimation(Length);
            //}
            //else DoAnimation(0);
        }
        protected float EvaluateCurve(float time)
        {
            if (Data.CustomCurve)
            {
                return Data.animationCurve.Evaluate(time * Data.Speed);
            }
            return animationCurve(time * Data.Speed);
        }

        public void Evaluate(float elapsedTime)
        {
            if (Data == null)
            {
                return;
            }
            if (reversed)
            {
                DoAnimation(Length - elapsedTime % Length);
            }
            else DoAnimation(elapsedTime % Length);
        }

        protected abstract void DoAnimation(float time);

        public abstract void SetStartData();
    }
}