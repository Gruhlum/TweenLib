using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

namespace HexTecGames.TweenLib
{
	[System.Serializable]
	public abstract class TweenTarget
	{
		public abstract List<TweenPlayData> GenerateTweenPlayData();
    }
}