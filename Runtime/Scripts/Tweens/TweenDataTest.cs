using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace HexTecGames.TweenLib
{
    [CreateAssetMenu(menuName = "HexTecGames/TestGame/TweenDataTest")]
    public class TweenDataPreset : ScriptableObject
    {
        [SerializeReference, SubclassSelector] public List<TweenData> tweenDatas;

        public List<TweenData> CreateCopy()
        {
            List<TweenData> results = new List<TweenData>();
            foreach (var data in tweenDatas)
            {
                results.Add(data.CreateShallowCopy());
            }
            return results;
        }
    }
}