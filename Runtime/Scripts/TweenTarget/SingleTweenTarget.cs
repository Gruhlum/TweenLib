using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

namespace HexTecGames.TweenLib
{
    [System.Serializable]
    public class SingleTweenTarget : TweenTarget
    {
        public TweenPreset animation;
        public GameObject targetGO;

        public override List<TweenPlayDataGroup> GenerateTweenPlayData()
        {
            return new List<TweenPlayDataGroup>() { animation.GenerateTweenPlayData(targetGO) };
        }
    }
}