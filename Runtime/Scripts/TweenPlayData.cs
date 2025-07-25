using System;
using System.Collections.Generic;
using UnityEngine;

namespace HexTecGames.TweenLib
{
    [System.Serializable]
    public class TweenPlayData
    {
        private List<Tween> tweens;

        private float timer = 0;

        private float duration;

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

            foreach (Tween tween in tweens)
            {
                tween.SetStartData();
            }

            foreach (Tween tween in tweens)
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
            foreach (Tween tween in tweens)
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
                foreach (Tween tween in tweens)
                {
                    tween.Stop();
                }
                OnFinishedPlaying?.Invoke(this);
                return true;
            }
            foreach (Tween tween in tweens)
            {
                tween.Evaluate(timer);
            }
            return false;
        }
        public void AddDelay(float delay, Position position)
        {
            foreach (Tween tween in tweens)
            {
                tween.AddDelay(delay, position);
            }
            SetDuration();
        }
        public void Stop()
        {
            IsPlaying = false;
            timer = 0;
            foreach (Tween tween in tweens)
            {
                tween.Stop();
            }
            OnFinishedPlaying?.Invoke(this);
        }
        public void Start(bool reverse)
        {
            if (timer > 0)
            {
                timer = duration - timer;
            }

            foreach (Tween tween in tweens)
            {
                tween.Start(reverse);
            }

            if (IsPlaying && timer <= 0)
            {
                // We recieved two inputs on the same frame
                Stop();
            }
            else IsPlaying = true;
        }
        public void MoveToEnd()
        {
            foreach (Tween tween in tweens)
            {
                tween.MoveToEnd();
            }
        }
        public void MoveToStart()
        {
            foreach (Tween tween in tweens)
            {
                tween.MoveToStart();
            }
        }
        public void ResetStartDatas()
        {
            foreach (Tween tween in tweens)
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
            foreach (Tween tween in tweens)
            {
                tween.ResetEffect();
            }
        }
    }
}