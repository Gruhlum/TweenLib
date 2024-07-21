using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace HexTecGames.TweenLib
{
    public class TweenGroupPlayer : TweenPlayerBase
    {
        [SerializeField] public List<TweenInfo> animations;
        [SerializeField] private List<GameObject> targetGOs = default;

        public float Spacing
        {
            get
            {
                return spacing;
            }
            private set
            {
                spacing = value;
            }
        }
        [SerializeField, Tooltip("How many seconds of delay between each gameObject")] private float spacing;


        public void Play(GameObject go, bool reversed)
        {
            var results = tweenPlayDatas.FindAll(x => x.TargetGO == go);
            foreach (var result in results)
            {
                result.Start(reversed);
            }
        }

        public override void InitTweens()
        {
            base.InitTweens();
            AddSpacing();
        }

        private void AddSpacing()
        {
            if (spacing <= 0)
            {
                return;
            }
            if (tweenPlayDatas == null || tweenPlayDatas.Count <= 1)
            {
                return;
            }
            for (int i = 1; i < tweenPlayDatas.Count; i++)
            {
                tweenPlayDatas[i].AddDelay(i * spacing, Position.Start);
                tweenPlayDatas[i].AddDelay(i * spacing, Position.End);
            }
        }

        protected override List<TweenPlayData> GenerateTweenPlayDatas()
        {
            List<TweenPlayData> results = new List<TweenPlayData>();

            foreach (var targetGO in targetGOs)
            {
                foreach (var animation in animations)
                {
                    results.Add(animation.GenerateTweenPlayData(targetGO));
                }
            }
            return results;
        }
    }
}