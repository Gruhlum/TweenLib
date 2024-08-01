using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace HexTecGames.TweenLib
{
    [CreateAssetMenu(menuName = "HexTecGames/TweenLib/Animation"), CanEditMultipleObjects]
    public class TweenAnimation : TweenInfo
    {
        [SerializeReference, SubclassSelector] protected List<TweenData> tweenDatas = null;

        public override TweenPlayData GenerateTweenPlayData(GameObject go)
        {
            return new TweenPlayData(GenerateTweens(go), go);
        }
        public List<Tween> GenerateTweens(GameObject go)
        {
            List<Tween> results = new List<Tween>();
            foreach (var data in tweenDatas)
            {
                Tween t = data.Create();
                t.Init(go);
                results.Add(t);
            }
            return results;
        }
    }
}