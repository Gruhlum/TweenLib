using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace HexTecGames.TweenLib
{
	public class TweenAnim
	{
        public static IEnumerator LerpImageColor(Image img, float duration, Color startCol, Color endCol)
        {
            float timer = 0;
            while (timer < duration)
            {
                img.color = Color.Lerp(startCol, endCol, timer / duration);
                yield return null;
                timer += Time.deltaTime;
            }
        }
        public static IEnumerator LerpSRColor(SpriteRenderer sr, float duration, Color startCol, Color endCol)
        {
            float timer = 0;
            while (timer < duration)
            {
                sr.color = Color.Lerp(startCol, endCol, timer / duration);
                yield return null;
                timer += Time.deltaTime;
            }
        }
    }
}