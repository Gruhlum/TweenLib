using System.Collections.Generic;
using UnityEngine;

namespace HexTecGames.TweenLib
{
    //[CreateAssetMenu(menuName = "HexTecGames/TweenLib/Sequence")]
    public class TweenSequencePreset : TweenPresetBase
    {
        [SerializeField] private List<TweenSegment> segments;

        public override TweenPlayDataGroup GenerateTweenPlayData(GameObject go)
        {
            List<TweenPlayData> results = new List<TweenPlayData>();
            foreach (TweenSegment segment in segments)
            {
                results.Add(segment.CreateTweenPlayData(go));
            }
            return new TweenPlayDataGroup(results);
        }
    }
}