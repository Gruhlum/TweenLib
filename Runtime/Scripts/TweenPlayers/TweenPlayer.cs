using HexTecGames.Basics;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace HexTecGames.TweenLib
{
    public class TweenPlayer : MonoBehaviour
    {      
        [DrawIf("tweenDatas", 1, reverse: true)]
        [SerializeField] private TweenPreset preset = default;

        [SerializeField] private List<GameObject> gameObjects = default;

        [SerializeReference, SubclassSelector]
        protected List<TweenData> tweenDatas = null;
        private float timer;

        protected List<Tween> tweens = new List<Tween>();

        public event Action FinishedPlaying;

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
        [SerializeField] private bool deactivateGOsAfterPlay = default;

        public bool OnlyPlayOnce
        {
            get
            {
                return onlyPlayOnce;
            }
            set
            {
                onlyPlayOnce = value;
            }
        }
        [SerializeField] private bool onlyPlayOnce = default;

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

        private bool initDone;

        private void OnValidate()
        {
            if (preset != null && (tweens == null || tweens.Count == 0))
            {
                LoadTweens(preset.TweenDatas);
                preset = null;
            }
            if (tweens == null || gameObject == null)
            {
                return;
            }
            if (tweenDatas != null)
            {
                foreach (var tweenData in tweenDatas)
                {
                    if (tweenData == null)
                    {
                        continue;
                    }
                    foreach (var go in gameObjects)
                    {
                        tweenData.AddRequiredComponents(go);
                    }
                }
            }
        }

        private void Start()
        {
            if (!initDone)
            {
                InitTweens();

                if (startOnEnabled == false)
                {
                    return;
                }
                if (tweens == null)
                {
                    return;
                }
                Play();
            }
        }

        private void Update()
        {
            if (IsPlaying == false)
            {
                return;
            }
            timer += Time.deltaTime * TimeScale;
            if (tweens != null)
            {
                foreach (var tween in tweens)
                {
                    if (tween != null && tween.IsFinished == false)
                    {
                        tween.Evaluate(timer);
                    }
                }
            }
            if (timer <= 0)
            {
                TweensFinishedPlaying();
            }
            else if (timer >= Duration)
            {
                foreach (var tween in tweens)
                {
                    if (!tween.IsFinished)
                    {
                        UpdateDuration();
                        return;
                    }
                }
                TweensFinishedPlaying();
            }
        }
        private void Reset()
        {
            if (gameObjects == null)
            {
                gameObjects = new List<GameObject>();
            }
            if (gameObjects.Count == 0)
            {
                gameObjects.Add(gameObject);
            }
        }

        private void OnEnable()
        {
            if (!initDone)
            {
                return;
            }
            timer = 0;
            if (startOnEnabled == false)
            {
                return;
            }
            if (tweens == null)
            {
                return;
            }
            Play();
        }
        private void OnDisable()
        {
            if (IsPlaying)
            {
                foreach (var tween in tweens)
                {
                    tween.Stop();
                }
                TweensFinishedPlaying();
            }
        }
        private void TweensFinishedPlaying()
        {
            IsPlaying = false;

            if (deactivateGOsAfterPlay)
            {
                foreach (var go in gameObjects)
                {
                    go.SetActive(false);
                }
            }
            FinishedPlaying?.Invoke();
            if (OnlyPlayOnce)
            {
                this.enabled = false;
            }
        }

        public List<TweenData> GetTweenData()
        {
            return tweenDatas;
        }
        public void AddGameObject(GameObject GO)
        {
            if (GO == null)
            {
                return;
            }
            if (gameObjects.Contains(GO))
            {
                return;
            }
            gameObjects.Add(GO);
            foreach (var data in tweenDatas)
            {
                if (!data.IsEnabled)
                {
                    continue;
                }
                Tween t = data.Create();
                t.Init(GO);
                tweens.Add(t);
            }
            UpdateDuration();
        }
        public void AddGameObjects(List<GameObject> GOs)
        {
            foreach (var go in GOs)
            {
                AddGameObject(go);
            }
        }
        public void SetGameObject(GameObject go)
        {
            this.gameObjects = new List<GameObject>() { go };
            InitTweens();
        }
        public void SetGameObjects(List<GameObject> GOs)
        {
            this.gameObjects = new List<GameObject>();
            this.gameObjects.AddRange(GOs);
            InitTweens();
        }
        public void ResetStartValues()
        {
            foreach (var tween in tweens)
            {
                tween.SetStartData();
            }
        }
        private void InitTweens()
        {
            initDone = true;
            tweens.Clear();
            if (gameObjects == null)
            {
                Duration = 0;
                return;
            }
            foreach (var go in gameObjects)
            {
                if (go == null)
                {
                    continue;
                }
                foreach (var data in tweenDatas)
                {
                    if (data == null || !data.IsEnabled)
                    {
                        continue;
                    }
                    Tween t = data.Create();
                    t.Init(go);
                    tweens.Add(t);
                }
            }
            UpdateDuration();
        }
        public void LoadTweens(List<TweenData> tweenData)
        {
            tweenDatas = tweenData;
            InitTweens();
        }
        public void LoadTweens(List<TweenData> tweenData, List<GameObject> GOs)
        {
            if (GOs == null || GOs.Count == 0)
            {
                return;
            }

            this.gameObjects = new List<GameObject>();
            this.gameObjects.AddRange(GOs);

            LoadTweens(tweenData);
            if (startOnEnabled)
            {
                Play();
            }
        }
        public void UpdateDuration()
        {
            if (tweenDatas == null || tweenDatas.Count == 0)
            {
                Duration = 0;
                return;
            }
            if (tweenDatas.Any(x => x.Loop))
            {
                Duration = null;
            }
            else Duration = tweens.GetDuration();
        }

        [ContextMenu("Play")]
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
            if (gameObjects == null || gameObjects.Count == 0)
            {
                return;
            }
            if (!Duration.HasValue)
            {
                UpdateDuration();
            }
            timer = TimeScale > 0 ? 0 : Duration.Value;
            foreach (var tween in tweens)
            {
                tween.Start(reversed);
            }
            IsPlaying = true;
        }
        //[ContextMenu("Round Animation Keys")]
        //public void RoundAnimationKeys()
        //{
        //    foreach (var tween in tweens)
        //    {
        //        for (int i = 0; i < tween.animationCurve.keys.Length; i++)
        //        {
        //            Keyframe frame = tween.animationCurve.keys[i];
        //            if (frame.value - Mathf.RoundToInt(frame.value) < 0.1f && frame.value - Mathf.RoundToInt(frame.value) > 0 ||
        //                Mathf.RoundToInt(frame.value) - frame.value < 0.1f && Mathf.RoundToInt(frame.value) - frame.value > 0)
        //            {
        //                frame.value = Mathf.RoundToInt(frame.value);
        //            }
        //            if (frame.time - Mathf.RoundToInt(frame.time) < 0.1f && frame.time - Mathf.RoundToInt(frame.time) > 0 ||
        //                Mathf.RoundToInt(frame.time) - frame.time < 0.1f && Mathf.RoundToInt(frame.time) - frame.time > 0)
        //            {
        //                frame.time = Mathf.RoundToInt(frame.time);
        //            }
        //            tween.animationCurve.RemoveKey(i);
        //            tween.animationCurve.AddKey(frame);
        //        }
        //    }
        //}
    }
}