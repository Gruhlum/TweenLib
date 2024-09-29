using HexTecGames.TweenLib;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace HexTecGames
{
    [System.Serializable]
    public class TweenOverrideData
    {
        public TweenDataPreset parent;

        [SerializeReference, SubclassSelector] public List<TweenData> actualDatas;

        public void LoadParent()
        {
            if (parent == null)
            {
                return;
            }

            actualDatas = new List<TweenData>();

            foreach (var data in parent.tweenDatas)
            {
                var result = data.CreateShallowCopy();
                actualDatas.Add(result);
            }
        }
    }
}