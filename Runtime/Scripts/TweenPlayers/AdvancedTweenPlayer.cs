using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

namespace HexTecGames.TweenLib
{
    public class AdvancedTweenPlayer : TweenPlayerBase
    {
        [SerializeField] private List<TweenItem> tweenItems = default;

        protected override List<TweenPlayData> GenerateTweenPlayDatas()
        {
            List<TweenPlayData> results = new List<TweenPlayData>();
            foreach (var tweenItem in tweenItems)
            {
                results.Add(tweenItem.GenerateTweenPlayData());
            }
            return results;
        }
    }
}