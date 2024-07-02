using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace HexTecGames.TweenLib
{
    public class TweenGroupPlayer : TweenPlayerBase
    {
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


        public override void InitTweens()
        {
            base.InitTweens();
            if (spacing <= 0)
            {
                return;
            }
            if (tweenPlayDatas == null ||tweenPlayDatas.Count <= 1)
            {
                return;
            }
            for (int i = 1; i < tweenPlayDatas.Count; i++)
            {
                tweenPlayDatas[i].AddDelay(i * spacing, Position.Start);
                tweenPlayDatas[i].AddDelay(i * spacing, Position.End);
            }
        }

        protected override List<GameObject> GetTargetGameObjects()
        {
            return targetGOs;
        }
    }
}