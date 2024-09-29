using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace HexTecGames.TweenLib
{
    [System.Serializable]
    public class TweenPlayData
    {
        private List<Tween> tweens;
        
        private float timer = 0;

        float duration;

        public bool IsEndless
        {
            get
            {
                return isEndless;
            }
            private set
            {
                isEndless = value;
            }
        }
        private bool isEndless;

        public bool IsPlaying
        {
            get
            {
                return isPlaying;
            }
            private set
            {
                isPlaying = value;
            }
        }
        [SerializeField] private bool isPlaying;

        public event Action<TweenPlayData> OnFinishedPlaying;

        public TweenPlayData(List<Tween> tweens)
        {
            this.tweens = tweens;

            foreach (var tween in tweens)
            {
                tween.SetStartData();
            }

            foreach (var tween in tweens)
            {
                if (tween.Data.EndlessLoop)
                {
                    IsEndless = true;
                    break;
                }
            }
            SetDuration();
        }

        private void SetDuration()
        {
            duration = 0;
            IsEndless = false;
            foreach (var tween in tweens)
            {
                if (tween.Data.EndlessLoop)
                {
                    duration = -1;
                    IsEndless = true;
                    break;
                }
                else if (tween.Duration > duration)
                {
                    duration = tween.Duration;
                }
            }
        }

        //private void Init(GameObject go)
        //{
        //    foreach (var tween in tweens)
        //    {
        //        tween.Init(go);
        //    }
        //}
        /// <summary>
        /// 
        /// </summary>
        /// <param name="timeStep"></param>
        /// <returns>True if it finished playing, otherwise False.</returns>
        public bool Evaluate(float timeStep)
        {
            if (!IsPlaying)
            {
                return true;
            }
            timer += timeStep;

            if (!IsEndless && timer >= duration)
            {
                IsPlaying = false;
                foreach (var tween in tweens)
                {
                    tween.Stop();
                }
                OnFinishedPlaying?.Invoke(this);
                return true;
            }
            foreach (var tween in tweens)
            {
                tween.Evaluate(timer);
            }
            return false;
        }
        public void AddDelay(float delay, Position position)
        {
            foreach (var tween in tweens)
            {
                tween.AddDelay(delay, position);
            }
            SetDuration();
        }
        public void Stop()
        {
            IsPlaying = false;
            timer = 0;
            foreach (var tween in tweens)
            {
                tween.Stop();
            }
            OnFinishedPlaying?.Invoke(this);
        }
        public void Start(bool reverse)
        {            
            IsPlaying = true;
            if (timer > 0)
            {
                timer = duration - timer;
            }

            foreach (var tween in tweens)
            {
                tween.Start(reverse);
            }
        }
        public void ResetStartDatas()
        {
            foreach (var tween in tweens)
            {
                tween.SetStartData();
            }
        }      

        public void ResetEffect()
        {
            if (tweens == null)
            {
                return;
            }
            foreach (var tween in tweens)
            {
                tween.ResetEffect();
            }
        }
    }
}