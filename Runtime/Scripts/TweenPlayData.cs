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

        public TweenPlayData NextData
        {
            get
            {
                return this.nextData;
            }
            set
            {
                this.nextData = value;
            }
        }
        private TweenPlayData nextData;

        private GameObject targetGO;
        private float timer = 0;

        private List<Tween> oldTweens = new List<Tween>();

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

        private float duration;
        // private bool reverse;

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

        public TweenPlayData(List<Tween> tweens, GameObject targetGO)
        {
            this.tweens = tweens;
            this.targetGO = targetGO;

            oldTweens.AddRange(tweens);
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

        private void InitNextPlayData()
        {
            tweens = NextData.tweens;
            SetDuration();
            NextData = NextData.NextData;
            Init(targetGO);
            timer = 0;
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
                if (tween.Length > duration)
                {
                    duration = tween.Length;
                }
            }
        }

        private void Init(GameObject go)
        {
            targetGO = go;

            foreach (var tween in tweens)
            {
                tween.Init(go);
            }

            Debug.Log("Duration: " + duration);
            oldTweens.AddRange(tweens);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="timeStep"></param>
        /// <returns>False if it finished playing, otherwise True.</returns>
        public void Evaluate(float timeStep)
        {
            if (!IsPlaying)
            {
                return;
            }

            timer += timeStep;

            if (!IsEndless && timer >= duration)
            {
                if (NextData != null)
                {
                    InitNextPlayData();
                }
                else
                {
                    IsPlaying = false;
                    foreach (var tween in tweens)
                    {
                        tween.Stop();
                    }
                    return;
                }
            }
            foreach (var tween in tweens)
            {
                tween.Evaluate(timer);
            }
        }
        public void AddDelay(float delay)
        {
            foreach (var tween in tweens)
            {
                tween.AddDelay(delay);
            }
        }
        public void Start(bool reverse)
        {
            IsPlaying = true;
            if (timer > 0)
            {
                timer = duration - timer;
            }
            
            //this.reverse = reverse;
            foreach (var tween in tweens)
            {
                tween.Start(reverse);
            }
        }

        public void ReverseEffect()
        {
            if (oldTweens == null)
            {
                return;
            }
            for (int i = oldTweens.Count - 1; i >= 0; i--)
            {
                oldTweens[i].ResetEffect();
            }
        }
    }
}