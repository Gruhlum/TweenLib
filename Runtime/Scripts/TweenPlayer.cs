using System;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;

namespace HexTecGames.TweenLib
{
    public class TweenPlayer : MonoBehaviour
    {
        [SerializeField] private GameObject go = default;
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
        public List<Tween> tweens = new List<Tween>();
        private float timer;

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

        private void Awake()
        {
            if (tweens != null)
            {
                foreach (var tween in tweens)
                {
                    tween.Init(go);
                    tween.FinishedPlaying += Tween_FinishedPlaying;
                }
            }
        }

        private void Tween_FinishedPlaying(Tween tween)
        {
            if (tweens.Any(x => x.IsFinished == false))
            {
                return;
            }

            IsPlaying = false;
            FinishedPlaying?.Invoke();
            foreach (var t in tweens)
            {
                t.Reverse = false;
            }
        }

        private void Reset()
        {
            go = gameObject;
        }

        private void Update()
        {
            if (IsPlaying == false)
            {
                return;
            }
            timer += Time.deltaTime;
            if (tweens != null)
            {
                foreach (var tween in tweens)
                {
                    if (tween != null && tween.IsFinished == false && tween.IsEnabled == true)
                    {
                        tween.Evaluate(timer);
                    }
                }
            }
        }

        private void OnEnable()
        {
            timer = 0;
            if (startOnEnabled)
            {
                IsPlaying = true;
                tweens.ForEach(x => x.IsFinished = false);
                tweens.ForEach(x => x.IsEnabled = true);
            }
            if (tweens != null)
            {
                foreach (var tween in tweens)
                {
                    if (tween != null)
                    {
                        tween.Evaluate(0);
                    }
                }
            }
        }
        public float GetDuration()
        {
            return tweens.Max(x => x.Duration);
        }
        public void Play(bool reversed = false)
        {
            timer = 0;
            foreach (var tween in tweens)
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