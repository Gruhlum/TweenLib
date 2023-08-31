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
                return data.animationCurve.keys[data.animationCurve.length - 1].time;
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

        private bool Reversed
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

        public void SetStartValues()
        {
            Init(targetGO);
        }
        public virtual void Init(GameObject go)
        {
            targetGO = go;
            SetStartData();
        }

        public Tween()
        {
        }

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
        }
        public void Stop()
        {
            if (Data == null)
            {
                return;
            }
            temporaryReverse = false;
            IsFinished = true;
            if (Reversed)
            {
                DoAnimation(0);
            }
            else DoAnimation(Length);
        }

        protected float EvaluateCurve(float time)
        {
            return data.animationCurve.Evaluate(time);
        }

        public void Evaluate(float elapsedTime)
        {
            if (Data == null)
            {
                return;
            }
            this.elapsedTime = elapsedTime;
            if (IsFinished || data.IsEnabled == false)
            {
                return;
            }
            if (data.Speed <= 0)
            {
                return;
            }
            if (data.Delay > elapsedTime)
            {
                return;
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

        protected abstract void SetStartData();
    }
}