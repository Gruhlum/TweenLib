using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

namespace HexTecGames.TweenLib
{
    [System.Serializable]
    public class GroupTweenTarget : TweenTarget
    {
        [SubclassSelector, SerializeReference]public List<TweenData> animations = new List<TweenData>();
        public List<GameObject> targetGOs = new List<GameObject>();

        public override List<TweenPlayDataGroup> GenerateTweenPlayData()
        {
            List<TweenPlayDataGroup> results = new List<TweenPlayDataGroup>();
            foreach (var targetGO in targetGOs)
            {
                if (targetGO == null)
                {
                    Debug.Log("Target GameObject is null!");
                    continue;
                }
                foreach (var animation in animations)
                {
                    if (animation == null)
                    {
                        Debug.Log("No Animation data!");
                        continue;
                    }
                    //results.Add(animation.GenerateTweenPlayData(targetGO));
                }
            }
            return results;
        }
    }
}