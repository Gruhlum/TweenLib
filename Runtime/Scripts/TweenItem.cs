using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

namespace HexTecGames.TweenLib
{
	[System.Serializable]
	public class TweenItem
	{
		public TweenAnimation animation;
		public GameObject targetGO;

		public TweenPlayData GenerateTweenPlayData()
		{
			return animation.GenerateTweenPlayData(targetGO);
		}
	}
}