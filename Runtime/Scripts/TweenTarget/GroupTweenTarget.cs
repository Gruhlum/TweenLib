using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

namespace HexTecGames.TweenLib
{
    [System.Serializable]
    public class GroupTweenTarget : TweenTarget
    {
        public List<TweenInfo> animations = new List<TweenInfo>();
        public List<GameObject> targetGOs = new List<GameObject>();

        public override List<TweenPlayData> GenerateTweenPlayData()
        {
            List<TweenPlayData> results = new List<TweenPlayData>();
            foreach (var targetGO in targetGOs)
            {
                foreach (var animation in animations)
                {
                    results.Add(animation.GenerateTweenPlayData(targetGO));
                }
            }
            return results;
        }
    }
}