using System.Collections.Generic;
using UnityEngine;

namespace HexTecGames.TweenLib
{
    //[CreateAssetMenu(menuName = "HexTecGames/TweenLib/TweenDataTest")]
    public class TweenDataPreset : ScriptableObject
    {
        [SerializeReference, SubclassSelector] public List<TweenData> tweenDatas;

        public List<TweenData> CreateCopy()
        {
            List<TweenData> results = new List<TweenData>();
            foreach (TweenData data in tweenDatas)
            {
                results.Add(data.CreateShallowCopy());
            }
            return results;
        }
    }
}