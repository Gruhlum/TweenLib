using System.Collections.Generic;
using UnityEngine;

namespace HexTecGames.TweenLib
{
    [System.Serializable]
    public class TweenSegment
    {
        [SerializeField] private List<TweenPreset> animations;
        [Min(0), SerializeField] private float delay;

        public TweenPlayData CreateTweenPlayData(GameObject go)
        {
            List<Tween> results = new List<Tween>();

            foreach (TweenPreset animation in animations)
            {
                results.AddRange(animation.GenerateTweens(go));
            }
            TweenPlayData playData = new TweenPlayData(results);
            playData.AddDelay(delay, Position.Start);
            return playData;
        }
    }
}