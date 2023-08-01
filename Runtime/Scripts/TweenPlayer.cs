using System;
using System.Collections.Generic;
using System.Linq;
using Unity.Plastic.Antlr3.Runtime;
using UnityEditor;
using UnityEngine;

namespace HexTecGames.TweenLib
{
    public class TweenPlayer : MonoBehaviour
    {
        [SerializeField] private List<GameObject> GOs = default;
        public bool StartOnEnabled
        {
            get
            {
                return startOnEnabled;
            }
            set
            {
                startOnEnabled = value;
            }
        }
        [SerializeField] private bool startOnEnabled = true;

        [SerializeReference, SubclassSelector]
        protected List<Tween> tweens = new List<Tween>();
        private float timer;

        protected List<Tween> actualTweens = new List<Tween>();

        public event Action FinishedPlaying;

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
        private bool isPlaying;

        [SerializeField] private bool deactivateGOsAfterPlay = default;

        //TODO deleta
        public bool DestroyAfterPlay
        {
            get
            {
                return destroyAfterPlay;
            }
            set
            {
                destroyAfterPlay = value;
            }
        }
        [SerializeField] private bool destroyAfterPlay = default;


        public float? Duration
        {
            get
            {
                return duration;
            }
            set
            {
                duration = value;
            }
        }
        private float? duration = default;


        private void Awake()
        {
            InitTweens();
        }

        private void Update()
        {
            if (IsPlaying == false)
            {
                return;
            }
            timer += Time.deltaTime;

            if (actualTweens != null)
            {
                foreach (var tween in actualTweens)
                {
                    if (tween != null && tween.IsFinished == false)
                    {
                        tween.Evaluate(timer);
                    }
                }
            }
            if (timer >= Duration)
            {
                TweensFinishedPlaying();
            }
        }

        private void OnEnable()
        {
            timer = 0;
            if (startOnEnabled == false)
            {
                return;
            }
            if (actualTweens == null)
            {
                return;
            }
            IsPlaying = true;
            actualTweens.ForEach(x => x.IsFinished = false);
            actualTweens.ForEach(x => x.IsEnabled = true);

            foreach (var tween in actualTweens)
            {
                if (tween != null)
                {
                    tween.Evaluate(0);
                }
            }
        }
        private void TweensFinishedPlaying()
        {
            IsPlaying = false;
            if (destroyAfterPlay)
            {
                Destroy(this);
                return;
            }
            foreach (var t in actualTweens)
            {
                t.Reverse = false;
            }
            
            if (deactivateGOsAfterPlay)
            {
                foreach (var go in GOs)
                {
                    go.SetActive(false);
                }
            }
            FinishedPlaying?.Invoke();
        }

        public List<TweenData> GetTweenData()
        {
            List<TweenData> datas = new List<TweenData>();
            foreach (var tween in tweens)
            {
                datas.Add(tween.GetData());
            }
            return datas;
        }
        public void SetGameObject(GameObject go)
        {
            this.GOs = new List<GameObject>() { go };
            InitTweens();
        }
        public void SetGameObjects(List<GameObject> GOs)
        {
            this.GOs = new List<GameObject>();
            this.GOs.AddRange(GOs);
            InitTweens();
        }
        private void InitTweens()
        {
            actualTweens.Clear();
            if (GOs == null)
            {
                Duration = 0;
                return;
            }
            foreach (var go in GOs)
            {
                if (go == null)
                {
                    continue;
                }
                foreach (var original in tweens)
                {
                    if (!original.IsEnabled)
                    {
                        continue;
                    }
                    Tween t = original.GetData().Create();
                    t.Init(go);
                    actualTweens.Add(t);
                }
            }
            UpdateDuration();
        }
        public void LoadTweens(List<TweenData> tweenData)
        {           
            if (tweenData == null)
            {
                return;
            }
            tweens = new List<Tween>();

            foreach (var data in tweenData)
            {
                Tween t = data.Create();
                tweens.Add(t);
            }
            InitTweens();
        }
        public void LoadTweens(List<TweenData> tweenData, List<GameObject> GOs)
        {
            if (GOs == null || GOs.Count == 0)
            {
                return;
            }

            this.GOs = new List<GameObject>();
            this.GOs.AddRange(GOs);

            LoadTweens(tweenData);
        }
        public void UpdateDuration()
        {
            if (tweens == null || tweens.Count == 0)
            {
                Duration = 0;
                return;
            }
            if (tweens.Any(x => x.Loop))
            {
                Duration = null;
            }
            else Duration = tweens.Max(x => x.Duration);
        }
        [ContextMenu("Reset and Play")]
        public void ResetAndPlay()
        {
            InitTweens();
            Play(false);
        }
        public void Play()
        {
            Play(false);
        }
        public void PlayReversed()
        {
            Play(true);
        }
        private void Play(bool reversed = false)
        {
            if (GOs == null || GOs.Count == 0)
            {
                return;
            }
            timer = 0;
            foreach (var tween in actualTweens)
            {
                tween.Reverse = reversed;
                tween.IsFinished = false;
            }
            IsPlaying = true;
        }

        [ContextMenu("Round Animation Keys")]
        public void RoundAnimationKeys()
        {
            foreach (var tween in tweens)
            {
                for (int i = 0; i < tween.animationCurve.keys.Length; i++)
                {
                    Keyframe frame = tween.animationCurve.keys[i];
                    if (frame.value - Mathf.RoundToInt(frame.value) < 0.1f && frame.value - Mathf.RoundToInt(frame.value) > 0 ||
                        Mathf.RoundToInt(frame.value) - frame.value < 0.1f && Mathf.RoundToInt(frame.value) - frame.value > 0)
                    {
                        frame.value = Mathf.RoundToInt(frame.value);
                    }
                    if (frame.time - Mathf.RoundToInt(frame.time) < 0.1f && frame.time - Mathf.RoundToInt(frame.time) > 0 ||
                        Mathf.RoundToInt(frame.time) - frame.time < 0.1f && Mathf.RoundToInt(frame.time) - frame.time > 0)
                    {
                        frame.time = Mathf.RoundToInt(frame.time);
                    }
                    tween.animationCurve.RemoveKey(i);
                    tween.animationCurve.AddKey(frame);
                }
            }
        }
    }
}