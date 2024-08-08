using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace HexTecGames.TweenLib
{
	public abstract class TweenPresetBase : ScriptableObject
	{
		public abstract TweenPlayDataGroup GenerateTweenPlayData(GameObject go);
	}
}