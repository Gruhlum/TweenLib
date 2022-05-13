using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace HexTecGames.TweenLib
{
	[System.Serializable]
	public class TweenDataGroup
	{
		public int Index;
		[SerializeReference]
		public List<TweenData> datas = new List<TweenData>();
	}
}