using System.Collections.Generic;
using HexTecGames.TweenLib;
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

            foreach (TweenData data in parent.tweenDatas)
            {
                TweenData result = data.CreateShallowCopy();
                actualDatas.Add(result);
            }
        }
    }
}