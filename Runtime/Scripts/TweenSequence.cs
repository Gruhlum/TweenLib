using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace HexTecGames.TweenLib
{
    [CreateAssetMenu(menuName = "HexTecGames/TweenLib/Sequence")]
    public class TweenSequence : TweenInfo
    {
        [SerializeField] private List<TweenSegment> segments;

        public override TweenPlayData GenerateTweenPlayData(GameObject go)
        {
            TweenPlayData firstData = null;
            TweenPlayData previousData = null;

            foreach (var segment in segments)
            {
                TweenPlayData tweenPlayData = segment.CreateTweenPlayData(go);
                if (firstData == null)
                {
                    firstData = tweenPlayData;
                }
                if (previousData != null)
                {
                    previousData.NextData = tweenPlayData;
                }
                previousData = tweenPlayData;
            }
            return firstData;
        }
    }
}