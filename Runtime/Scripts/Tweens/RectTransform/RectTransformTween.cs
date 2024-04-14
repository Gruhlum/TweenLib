using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace HexTecGames.TweenLib
{
	public abstract class RectTransformTween : VectorTween
	{
        protected RectTransform rectTransform;

        protected RectTransformTween(VectorData data) : base(data)
        {
        }
    }
}