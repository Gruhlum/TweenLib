using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace HexTecGames.TweenLib
{
    public class TweenPreset : ScriptableObject
    {
        [SerializeReference, SubclassSelector]

        public List<TweenData> TweenDatas = new List<TweenData>();

        public void Apply(GameObject parent, List<GameObject> affectedGOs, bool destroyAfterPlay)
        {
            TweenPlayer player = parent.AddComponent<TweenPlayer>();
            player.LoadTweens(TweenDatas, affectedGOs);
            player.DestroyAfterPlay = destroyAfterPlay;
        }
        public void Apply(GameObject go, bool destroyAfterPlay)
        {
            TweenPlayer player = go.AddComponent<TweenPlayer>();
            player.LoadTweens(TweenDatas, new List<GameObject>() { go });
            player.DestroyAfterPlay = destroyAfterPlay;
        }
    }
}