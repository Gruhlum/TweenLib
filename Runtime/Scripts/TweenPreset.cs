using HexTecGames.Basics;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace HexTecGames.TweenLib
{
    public class TweenPreset : ScriptableObject
    {
        [SerializeReference, SubclassSelector]

        public List<TweenData> TweenDatas = new List<TweenData>();

        public void Apply(GameObject parent, List<GameObject> affectedGOs)
        {
            TweenPlayer player = parent.AddComponent<TweenPlayer>();
            player.LoadTweens(TweenDatas, affectedGOs);
        }
        public void Apply(GameObject go)
        {
            TweenPlayer player = go.AddComponent<TweenPlayer>();
            player.LoadTweens(TweenDatas, new List<GameObject>() { go });
        }
    }
}