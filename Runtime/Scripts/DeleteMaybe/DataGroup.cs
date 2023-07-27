using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace HexTecGames.TweenLib
{
	[System.Serializable]
	public class DataGroup
	{
		public List<TweenContainer> datas;

		public bool IsCompleted()
        {
            foreach (var data in datas)
            {
                if (!data.Tween.IsFinished)
                {
                    return false;
                }
            }
            return true;
        }
	}
}