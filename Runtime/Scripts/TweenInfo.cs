using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace HexTecGames.TweenLib
{
	public abstract class TweenInfo : ScriptableObject
	{
		public abstract TweenPlayData GenerateTweenPlayData(GameObject go);
	}
}