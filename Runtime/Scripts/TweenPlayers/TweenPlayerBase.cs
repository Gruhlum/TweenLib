using HexTecGames.Basics;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace HexTecGames.TweenLib
{
    public abstract class TweenPlayerBase : MonoBehaviour
    {
        //TODO:
        // ToggleTweenPlayer working
        // GroupTweenPlayer working

        [SerializeField] public List<TweenInfo> animations;

        protected List<TweenPlayData> tweenPlayDatas;

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

        protected void Start()
        {
            if (!PlayOnEnable)
            {
                Deactivate();
            }
        }

        protected void Update()
        {
            AdvanceTime(Time.deltaTime);
        }

        protected void OnEnable()
        {
            if (!tweensAreInitialized)
            {
                InitTweens();
            }

            foreach (var playData in tweenPlayDatas)
            {
                playData.Start(false);
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
                this.enabled = false;
                OnDisabled?.Invoke(this);
            }
        }
        protected abstract List<GameObject> GetTargetGameObjects();
        public virtual void InitTweens()
        {
            tweensAreInitialized = true;

            tweenPlayDatas = new List<TweenPlayData>();
            List<GameObject> targetGOs = GetTargetGameObjects();

            foreach (var targetGO in targetGOs)
            {
                tweenPlayDatas.AddRange(GetTweenPlayData(targetGO));
            }

            if (tweenPlayDatas == null)
            {
                Debug.Log("No tweens");
                Deactivate();
            }
        }
        private List<TweenPlayData> GetTweenPlayData(GameObject go)
        {
            List<TweenPlayData> results = new List<TweenPlayData>();
            foreach (var anim in animations)
            {
                results.Add(anim.GenerateTweenPlayData(go));
            }
            return results;
        }

        public void Play(bool reversed)
        {
            this.enabled = true;
            foreach (var playData in tweenPlayDatas)
            {
                playData.Start(reversed);
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
    }
}