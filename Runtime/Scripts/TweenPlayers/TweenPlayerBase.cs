using HexTecGames.Basics;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace HexTecGames.TweenLib
{
    public abstract class TweenPlayerBase : MonoBehaviour
    {
        protected List<TweenPlayData> tweenPlayDatas;

        public bool IsActive
        {
            get
            {
                return isActive;
            }
            private set
            {
                isActive = value;
            }
        }
        private bool isActive;


        public bool PlayOnEnable
        {
            get
            {
                return this.playOnEnable;
            }
            set
            {
                this.playOnEnable = value;
            }
        }
        [SerializeField] private bool playOnEnable = true;

        public float TimeScale
        {
            get
            {
                return timeScale;
            }
            set
            {
                timeScale = value;
            }
        }
        [SerializeField] private float timeScale = 1;

        public event Action<TweenPlayerBase> OnDisabled;

        private bool tweensAreInitialized;

        protected virtual void Start()
        {
            if (!PlayOnEnable)
            {
                Deactivate();
            }
        }

        protected virtual void Update()
        {
            if (!IsActive)
            {
                return;
            }
            AdvanceTime(Time.deltaTime);
        }

        protected virtual void OnDisable()
        {
            if (IsActive)
            {
                StopAnimations();
            }
        }
        protected virtual void OnEnable()
        {
            if (!tweensAreInitialized)
            {
                InitTweens();
            }
            if (PlayOnEnable)
            {
                Play();
            }
        }
        public bool AdvanceTime(float timeStep)
        {
            for (int i = tweenPlayDatas.Count - 1; i >= 0; i--)
            {
                tweenPlayDatas[i].Evaluate(timeStep * TimeScale);
            }
            foreach (var tweenPlayData in tweenPlayDatas)
            {
                if (tweenPlayData.IsPlaying)
                {
                    return false;
                }
            }
            Deactivate();
            return true;
        }
        public void Deactivate()
        {
            if (Application.isPlaying)
            {
                IsActive = false;
                OnDisabled?.Invoke(this);
            }
        }

        public virtual void InitTweens()
        {
            tweensAreInitialized = true;

            tweenPlayDatas = GenerateTweenPlayDatas();

            if (tweenPlayDatas == null)
            {
                Debug.Log("No tweens");
                Deactivate();
            }
        }
        protected abstract List<TweenPlayData> GenerateTweenPlayDatas();

        public void Play(bool reversed = false)
        {
            IsActive = true;

            foreach (var playData in tweenPlayDatas)
            {
                playData.Start(reversed);
            }
        }
        public void SetAnimationToStart()
        {
            if (tweenPlayDatas == null)
            {
                return;
            }
            foreach (var playData in tweenPlayDatas)
            {
                playData.SetAnimationToStart();
            }
        }
        public void SetAnimationToEnd()
        {
            if (tweenPlayDatas == null)
            {
                return;
            }
            foreach (var playData in tweenPlayDatas)
            {
                playData.SetAnimationToEnd();
            }
        }
        public void ResetEffects()
        {
            if (tweenPlayDatas == null)
            {
                return;
            }
            foreach (var playData in tweenPlayDatas)
            {
                playData.ReverseEffect();
            }
        }
        public void StopAnimations()
        {
            if (tweenPlayDatas == null)
            {
                return;
            }
            foreach (var playData in tweenPlayDatas)
            {
                playData.Stop();
            }
            IsActive = false;
        }
        public void ResetStartData()
        {
            if (tweenPlayDatas == null)
            {
                return;
            }
            foreach (var playData in tweenPlayDatas)
            {
                playData.ResetStartDatas();
            }
        }
    }
}