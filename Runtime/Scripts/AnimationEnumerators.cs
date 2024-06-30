using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace HexTecGames.TweenLib
{
	public class AnimationEnumerators
	{
        public static IEnumerator LerpColor(Image img, float duration, Color startCol, Color endCol)
        {
            float timer = 0;
            while (timer < duration)
            {
                img.color = Color.Lerp(startCol, endCol, timer / duration);
                yield return null;
                timer += Time.deltaTime;
            }
        }
        public static IEnumerator LerpColor(SpriteRenderer sr, float duration, Color startCol, Color endCol)
        {
            float timer = 0;
            while (timer < duration)
            {
                sr.color = Color.Lerp(startCol, endCol, timer / duration);
                yield return null;
                timer += Time.deltaTime;
            }
        }
        public static IEnumerator FadeColorOut(SpriteRenderer sr, float duration, float startAlpha = 1f)
        {
            float timer = 0;
            Color col = sr.color;
            col.a = startAlpha;
            sr.color = col;
            while (timer < duration)
            {
                yield return null;
                timer += Time.deltaTime;
                col.a = (1 * startAlpha) - timer / duration * startAlpha;
                sr.color = col;
            }
        }
        public static IEnumerator FadeColorIn(SpriteRenderer sr, float duration, float endAlpha = 1f)
        {
            float timer = 0;
            Color col = sr.color;
            col.a = 0;
            sr.color = col;
            while (timer < duration)
            {
                yield return null;
                timer += Time.deltaTime;
                col.a = timer / duration / endAlpha;
                sr.color = col;
            }
        }
        public static IEnumerator FlashColor(SpriteRenderer sr, float duration, Color startColor, Color endColor, float repeats = 1)
        {
            for (int i = 0; i < repeats; i++)
            {
                sr.color = startColor;
                yield return new WaitForSeconds(duration);
                sr.color = endColor;
                yield return new WaitForSeconds(duration);
            }           
        }

        public static IEnumerator ToggleActivationAfterDelay(GameObject go, bool active, float delay)
        {
            yield return new WaitForSeconds(delay);
            go.SetActive(active);
        }
    }
}