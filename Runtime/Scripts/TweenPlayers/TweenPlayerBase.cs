using HexTecGames.Basics;
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

		[SerializeField] private List<TweenInfo> animations;

		private List<TweenPlayData> tweenPlayDatas;
    
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


        protected void Awake()
        {
            InitTweens();
            this.enabled = false;
        }

        protected void Update()
        {
			AdvanceTime(Time.deltaTime);
        }

        protected void OnEnable()
        {
            if (!PlayOnEnable)
            {
                return;
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
            }
        }
        protected abstract List<GameObject> GetTargetGameObjects();
        public void InitTweens()
        {
            tweenPlayDatas = new List<TweenPlayData>();
            List<GameObject> targetGOs = GetTargetGameObjects();

            foreach (var go in targetGOs)
            {
                if (go == null)
                {
                    continue;
                }
                foreach (var anim in animations)
                {
                    tweenPlayDatas.Add(anim.GenerateTweenPlayData(go));
                }
            }         
            if (tweenPlayDatas == null)
            {
                Debug.Log("No tweens");
                Deactivate();
            }
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