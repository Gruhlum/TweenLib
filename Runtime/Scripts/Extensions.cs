using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace HexTecGames.TweenLib
{
	public static class Extensions
	{
        public static float GetDuration(this List<Tween> tweens)
        {
            if (tweens == null || tweens.Count == 0)
            {
                return 0;
            }
            if (tweens.Any(x => x.Data.Loop))
            {
                return 0;
            }
            else return tweens.Max(x => x.Duration);
        }
    }
}