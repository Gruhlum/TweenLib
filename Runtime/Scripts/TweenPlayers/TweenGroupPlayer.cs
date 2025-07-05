using System.Collections.Generic;
using UnityEngine;

namespace HexTecGames.TweenLib
{
    public class GroupTweenPlayer : TweenPlayerBase
    {
        [SerializeField] public GroupTweenTarget groupTweenTarget;

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

        public override bool IsEndless
        {
            get
            {
                return groupTweenTarget.IsEndless;
            }
        }

        [SerializeField, Tooltip("How many seconds of delay between each gameObject")] private float spacing;

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

        protected override List<TweenPlayDataGroup> GenerateTweenPlayDatas()
        {
            List<TweenPlayDataGroup> results = new List<TweenPlayDataGroup>
            {
                new TweenPlayDataGroup(groupTweenTarget.Create())
            };
            return results;
        }
    }
}