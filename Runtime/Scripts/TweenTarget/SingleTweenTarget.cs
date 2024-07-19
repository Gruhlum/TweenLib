using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

namespace HexTecGames.TweenLib
{
    [System.Serializable]
    public class SingleTweenTarget : TweenTarget
    {
        public TweenAnimation animation;
        public GameObject targetGO;

        public override List<TweenPlayData> GenerateTweenPlayData()
        {
            return new List<TweenPlayData>() { animation.GenerateTweenPlayData(targetGO) };
        }
    }
}