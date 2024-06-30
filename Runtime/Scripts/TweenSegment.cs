using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace HexTecGames.TweenLib
{
    [System.Serializable]
    public class TweenSegment
    {
        [SerializeField] private List<TweenAnimation> animations;
        [Min(0), SerializeField] private float delay;

        public TweenPlayData CreateTweenPlayData(GameObject go)
        {
            List<Tween> results = new List<Tween>();

            foreach (var animation in animations)
            {
                results.AddRange(animation.GenerateTweens(go));                              
            }
            TweenPlayData playData = new TweenPlayData(results, go);
            playData.AddDelay(delay);
            return playData;
        }
    }
}