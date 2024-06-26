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
                return length;            
            }
            private set
            {
                length = value;
            }
        }
        private float length;
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

        public float Duration
        {
            get
            {
                return (Length / data.Speed) * (data.Repeats + 1) + data.Delay;
            }
        }

        public float AnimationTime
        {
            get
            {
                return (elapsedTime - data.Delay) * data.Speed;
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

        private bool temporaryReverse;
        private bool startDataIsSet;

        public bool Reversed
        {
            get
            {
                if (temporaryReverse)
                {
                    return !Data.Reverse;
                }
                else return Data.Reverse;
            }
        }

        private Func<float, float> animationCurve;

        public Tween(TweenData data) 
        {
            this.Data = data;           
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
            if (!data.customCurve)
            {
                animationCurve = AnimationData.GetFunction(data.animationType, data.curve);
                Length = 1;
            }
            else Length = data.animationCurve.keys[^0].time;
            
        }
        protected abstract void SetStartObject(GameObject go);

        public void Start(bool reversed = false)
        {
            if (Data == null)
            {
                return;
            }
            temporaryReverse = reversed;          
            IsFinished = false;

            if (data.ApplyImmediately || data.Delay <= 0)
            {
                if (Reversed)
                {
                    DoAnimation(Length);
                }
                else DoAnimation(0);
            }
            startDataIsSet = !data.SetStartDataBeforePlay;
        }
        public void Stop()
        {
            if (Data == null)
            {
                return;
            }
            IsFinished = true;

            if (Reversed)
            {
                DoAnimation(0);
            }
            else DoAnimation(Length);
        }
        public void ResetEffect()
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
            if (Data.customCurve)
            {
                return Data.animationCurve.Evaluate(time);
            }
            return animationCurve(time);
        }

        public void Evaluate(float elapsedTime)
        {
            if (Data == null)
            {
                return;
            }     
            if (IsFinished)
            {
                return;
            }
            if (!data.IsEnabled)
            {
                return;
            }
            if (data.Speed <= 0)
            {
                return;
            }

            this.elapsedTime = elapsedTime;

            if (data.Delay > elapsedTime)
            {
                return;
            }
            if (!startDataIsSet)
            {
                SetStartData();
                startDataIsSet = true;
            }
            if (data.Loop == false && AnimationTime >= Length * (data.Repeats + 1))
            {
                Stop();
                return;
            }
            else
            {
                if (Reversed)
                {
                    DoAnimation(Length - AnimationTime % Length);
                }
                else DoAnimation(AnimationTime % Length);
            }
        }       

        protected abstract void DoAnimation(float time);

        public abstract void SetStartData();
    }
}