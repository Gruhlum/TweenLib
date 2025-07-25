using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace HexTecGames.TweenLib
{
    public abstract class TweenPlayerBase : MonoBehaviour
    {
        protected List<TweenPlayDataGroup> tweenPlayDatas;

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
        public virtual bool ResetStartDataOnPlay
        {
            get
            {
                return resetStartDataOnPlay;
            }
            set
            {
                resetStartDataOnPlay = value;
            }
        }
        [SerializeField] protected bool resetStartDataOnPlay;

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

        public abstract bool IsEndless
        {
            get;
        }

        public event Action<TweenPlayerBase> OnDisabled;
        public event Action<TweenPlayerBase> OnStartPlaying;

        protected bool tweensAreInitialized;

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
                Stop();
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
            foreach (TweenPlayDataGroup tweenPlayData in tweenPlayDatas)
            {
                if (tweenPlayData.IsPlaying)
                {
                    return false;
                }
            }
            ReachedEndOfAnimation();
            return true;
        }
        protected virtual void ReachedEndOfAnimation()
        {
            Deactivate();
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
            //else ResetStartData();
        }
        protected abstract List<TweenPlayDataGroup> GenerateTweenPlayDatas();

        public IEnumerator PlayCoroutine(bool reversed = false)
        {
            Play(reversed);
            while (IsActive)
            {
                yield return null;
            }
        }
        public void Play(bool reversed = false, bool resetStartData = false)
        {
            if (!tweensAreInitialized)
            {
                InitTweens();
            }
            if (ResetStartDataOnPlay || resetStartData)
            {
                ResetStartData();
            }

            IsActive = true;

            if (tweenPlayDatas != null)
            {
                foreach (TweenPlayDataGroup playData in tweenPlayDatas)
                {
                    playData.Start(reversed);
                }
            }

            OnStartPlaying?.Invoke(this);
        }

        public void ResetEffects()
        {
            if (tweenPlayDatas == null)
            {
                return;
            }
            foreach (TweenPlayDataGroup playData in tweenPlayDatas)
            {
                playData.ResetEffect();
            }
        }
        public virtual void Stop()
        {
            if (tweenPlayDatas == null)
            {
                return;
            }
            foreach (TweenPlayDataGroup playData in tweenPlayDatas)
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
            foreach (TweenPlayDataGroup playData in tweenPlayDatas)
            {
                playData.ResetStartDatas();
            }
        }

        public void MoveToStart()
        {
            foreach (TweenPlayDataGroup item in tweenPlayDatas)
            {
                item.MoveToStart();
            }
        }
        public void MoveToEnd()
        {
            foreach (TweenPlayDataGroup item in tweenPlayDatas)
            {
                item.MoveToEnd();
            }
        }
    }
}