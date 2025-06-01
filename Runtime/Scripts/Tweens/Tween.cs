using HexTecGames.Basics;
using HexTecGames.EaseFunctions;
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
                if (Data.EndlessLoop)
                {
                    return -1;
                }
                return (Length * (Data.Repeats + 1) + (Data.LoopWaitTime * Data.Repeats)) + StartDelay;
            }
        }

        public float TotalAnimationLength
        {
            get
            {
                if (Data.Repeats == 0 || Data.LoopWaitTime == 0)
                {
                    return AnimationLength;
                }
                return AnimationLength + Data.LoopWaitTime;
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

        public Tween(TweenData data, Component component)
        {
            this.Data = data;
            StartDelay = data.StartDelay;
            EndDelay = data.EndDelay;
            UpdateAnimationCurve();
        }

        public void UpdateAnimationCurve()
        {
            if (!data.customCurve)
            {
                animationCurve = EaseFunction.GetFunction(data.easing, data.function);
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
        //protected abstract void SetStartObject(GameObject go);
        //protected abstract void SetStartObject(Component component);

        public void Start(bool reversed = false)
        {
            if (Data == null)
            {
                return;
            }

            temporaryReverse = reversed;

            //if (Reversed)
            //{
            //    DoAnimation(AnimationLength);
            //}
            //else DoAnimation(0);
        }
        public void Stop()
        {
            bool direction = Reversed;
            if (Data.LoopMode == LoopMode.Mirror && Data.Repeats % 2 != 0)
            {
                direction = !direction;
            }
            if (direction)
            {
                MoveToStart();
            }
            else MoveToEnd();
        }

        public void MoveToStart()
        {
            DoAnimation(0);
        }

        public void MoveToEnd()
        {
            DoAnimation(AnimationLength);
        }

        public abstract void ResetEffect();
        protected float GetAnimationCurveValue(float time)
        {
            if (Data.customCurve)
            {
                return Data.animationCurve.Evaluate(time);
            }
            else return animationCurve(time);
        }
        public void Evaluate(float currentTime)
        {
            elapsedTime = currentTime;
            if (Data == null)
            {
                return;
            }

            elapsedTime -= startDelay;
            elapsedTime *= Data.Speed;

            bool playDirection = Reversed;

            //                                       4      +  0.5 = 4.5         /           1.5 = 3
            int totalFinishedRepeats = Mathf.FloorToInt(elapsedTime / TotalAnimationLength);
            int totalRepeats = Mathf.FloorToInt((elapsedTime + Data.LoopWaitTime) / TotalAnimationLength);

            if (totalFinishedRepeats != totalRepeats)
            {
                float val = Mathf.Min((elapsedTime + Data.LoopWaitTime) % TotalAnimationLength, Data.LoopWaitTime);
                //Debug.Log(elapsedTime + " - " + (elapsedTime + Data.LoopWaitTime) % cycleLength + " - " + val);
                elapsedTime -= val;
            }
            elapsedTime -= Data.LoopWaitTime * totalFinishedRepeats;
            //elapsedTime -= loopWaitSubtract;


            //Debug.Log("Elapsed: " + elapsedTime + " Finished: " + totalFinishedRepeats + " Started: " + totalRepeats);


            //Debug.Log(elapsedTime + " - " +  totalLoopWaitTime);

            if (elapsedTime < 0)
            {
                elapsedTime = 0;
            }

            if ((Data.EndlessLoop || Data.Repeats > 0) && Data.LoopMode == LoopMode.Mirror)
            {
                bool isUneven = totalRepeats % 2 != 0;
                if (isUneven)
                {
                    playDirection = !playDirection;
                }
            }

            //Debug.Log(playDirection);

            if (playDirection)
            {
                //Debug.Log(elapsedTime + " - " + (AnimationLength - (elapsedTime) % AnimationLength));

                DoAnimation(AnimationLength - (elapsedTime) % AnimationLength);
            }
            else
            {
                //Debug.Log(elapsedTime + " - " + (elapsedTime % AnimationLength));
                DoAnimation(elapsedTime % AnimationLength);
            }
        }
        protected abstract void DoAnimation(float time);
        public abstract void SetStartData();
    }
}