using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace HexTecGames.TweenLib
{
    public class TweenPlayer : TweenPlayerBase
    {
        [SerializeField] public List<TweenInfo> animations;
        [SerializeField] private GameObject target;

        protected void Reset()
        {
            target = this.gameObject;
        }

        protected override List<TweenPlayData> GenerateTweenPlayDatas()
        {
            List<TweenPlayData> results = new List<TweenPlayData>();
            foreach (var anim in animations)
            {
                results.Add(anim.GenerateTweenPlayData(target));
            }
            return results;
        }
    }
}