using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using TMPro;
using UnityEngine;

namespace HexTecGames.TweenLib
{
    public class TweenHandler : MonoBehaviour
    {
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

        [SerializeField] private List<DataGroup> groups = default;
        private List<Tween> tweens = new List<Tween>();

        private int currentIndex = -1;
        private bool isEnabled = false;

        private float elapsedTime = 0;

        private void OnValidate()
        {
            foreach (var group in groups)
            {
                if (group.datas.Any(x => x.GameObject == null))
                {
                    foreach (var container in group.datas.FindAll(x => x.GameObject == null))
                    {
                        container.GameObject = gameObject;
                    }
                }
            }
        }
        private void Awake()
        {
            foreach (var group in groups)
            {
                foreach (var data in group.datas)
                {
                    data.GenerateTweens(tweens);
                }
            }
            foreach (Tween tween in tweens)
            {
                if (tween != null)
                {
                    tween.FinishedPlaying += Tween_FinishedPlaying;
                }              
            }
        }

        private void OnDestroy()
        {
            foreach (Tween tween in tweens)
            {
                if (tween != null)
                {
                    tween.FinishedPlaying -= Tween_FinishedPlaying;
                }               
            }
        }
        private void Tween_FinishedPlaying(Tween tween)
        {
            if (groups[currentIndex].IsCompleted())
            {
                EnableNextTweens();
            }
        }
        private void EnableNextTweens()
        {
            currentIndex++;
            if (groups.Count <= currentIndex)
            {
                isEnabled = false;
                return;
            }
            foreach (var data in groups[currentIndex].datas)
            {
                if (data != null && data.Tween != null)
                {
                    data.Tween.IsEnabled = true;
                }
                
            }
        }

        private void OnEnable()
        {
            elapsedTime = 0;
            if (StartOnEnabled)
            {
                StartAnimation();
            }
        }
        private void Update()
        {
            if (!isEnabled)
            {
                return;
            }
            elapsedTime += Time.deltaTime;
            foreach (var tween in tweens)
            {
                if (tween != null)
                {
                    tween.IsEnabled = true;
                    tween.Evaluate(elapsedTime);
                }              
            }
        }
        public void StartAnimation()
        {
            if (groups.Count > 0)
            {
                isEnabled = true;
            }
            EnableNextTweens();
        }
    }
}