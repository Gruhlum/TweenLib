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
                return (AnimationLength / Data.Speed);
            }
        }

        public float Duration
        {
            get
            {
                return (Length * (Data.Repeats + 1)) + StartDelay;
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

        public float StartDelay
        {
            get
            {
                return startDelay;
            }
            private set
            {
                startDelay = value;
            }
        }
        private float startDelay;

        public float EndDelay
        {
            get
            {
                return endDelay;
            }
            private set
            {
                endDelay = value;
            }
        }
        private float endDelay;


        public float AnimationTime
        {
            get
            {
                return (elapsedTime * data.Speed) - StartDelay;
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
            StartDelay = data.StartDelay;
            EndDelay = data.EndDelay;
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
            else AnimationLength = data.animationCurve.keys[^1].time;

        }
        public void AddDelay(float time, Position position)
        {
            if (position == Position.Start)
            {
                StartDelay += time;
            }
            else EndDelay += time;
        }
        protected abstract void SetStartObject(GameObject go);

        public void Start(bool reversed = false)
        {
            if (Data == null)
            {
                return;
            }

            temporaryReverse = reversed;

            if (Reversed)
            {
                DoAnimation(AnimationLength);
            }
            else DoAnimation(0);
        }

        public void Stop()
        {
            if (Reversed)
            {
                DoAnimation(0);
            }
            else DoAnimation(AnimationLength);
        }

        public abstract void ResetEffect();

        protected float GetAnimationCurveValue(float time)
        {
            if (Data.CustomCurve)
            {
                return Data.animationCurve.Evaluate(time);
            }
            return animationCurve(time);
        }

        public void Evaluate(float currentTime)
        {
            elapsedTime = currentTime;

            if (Data == null)
            {
                return;
            }
            //Debug.Log(elapsedTime);

            //if (Reversed)
            //{
            //    if (elapsedTime < EndDelay)
            //    {
            //        DoAnimation(Length);
            //        return;
            //    }
            //    if (elapsedTime > Duration - StartDelay)
            //    {
            //        DoAnimation(0);
            //        return;
            //    }
            //}
            //else 
            //{
            //    if (elapsedTime < StartDelay)
            //    {
            //        return;
            //    }
            //    if (elapsedTime > Duration - EndDelay)
            //    {
            //        return;
            //    }
            //}

            //Debug.Log(EndDelay + " - " + StartDelay + " - " + elapsedTime);

            

            elapsedTime -= startDelay;
            elapsedTime *= Data.Speed;
            if (elapsedTime < 0)
            {
                elapsedTime = 0;
            }
            if (elapsedTime > AnimationLength * (Data.Repeats + 1))
            {
                return;
            }


            if (Reversed)
            {
                DoAnimation(AnimationLength - (elapsedTime) % AnimationLength);
            }
            else
            {
                DoAnimation(elapsedTime % AnimationLength);
            }
        }

        protected abstract void DoAnimation(float time);

        public abstract void SetStartData();
    }
}