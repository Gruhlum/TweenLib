using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace HexTecGames.TweenLib
{
    public class AnimationEnumerators
    {
        public static IEnumerator Fade(CanvasGroup canvasGroup, float duration, float start, float end)
        {
            float timer = 0;
            while (timer < duration)
            {
                timer += Time.deltaTime;
                canvasGroup.alpha = Mathf.Lerp(start, end, timer / duration);
                yield return null;
            }
        }
        public static IEnumerator Fade(CanvasGroup canvasGroup, float duration, float end)
        {
            yield return Fade(canvasGroup, duration, canvasGroup.alpha, end);
        }
        public static IEnumerator FadeIn(CanvasGroup canvasGroup, float duration, float end = 1)
        {
            yield return Fade(canvasGroup, duration, end);
        }
        public static IEnumerator FadeOut(CanvasGroup canvasGroup, float duration, float end = 0)
        {
            yield return Fade(canvasGroup, duration, end);
        }

        public static IEnumerator LerpColor(Image target, float duration, Color start, Color end)
        {
            float timer = 0;
            while (timer < duration)
            {
                timer += Time.deltaTime;
                target.color = Color.Lerp(start, end, timer / duration);
                yield return null;
            }
        }
        public static IEnumerator LerpColor(SpriteRenderer target, float duration, Color start, Color end)
        {
            float timer = 0;
            while (timer < duration)
            {
                timer += Time.deltaTime;
                target.color = Color.Lerp(start, end, timer / duration);
                yield return null;
            }
        }

        public static IEnumerator Fade(Image target, float duration, float endAlpha)
        {
            yield return LerpColor(target, duration, target.color, target.color.GetColorWithAlpha(endAlpha));
        }
        public static IEnumerator Fade(SpriteRenderer target, float duration, float endAlpha)
        {
            yield return LerpColor(target, duration, target.color, target.color.GetColorWithAlpha(endAlpha));
        }
        public static IEnumerator FadeIn(Image target, float duration, float endAlpha = 1)
        {
            yield return Fade(target, duration, endAlpha);
        }
        public static IEnumerator FadeIn(SpriteRenderer target, float duration, float endAlpha = 1)
        {
            yield return Fade(target, duration, endAlpha);
        }
        public static IEnumerator FadeOut(Image target, float duration, float endAlpha = 0)
        {
            yield return Fade(target, duration, endAlpha);
        }
        public static IEnumerator FadeOut(SpriteRenderer target, float duration, float endAlpha = 0)
        {
            yield return Fade(target, duration, endAlpha);
        }

        public static IEnumerator FlashColor(SpriteRenderer target, float duration, Color startColor, Color endColor, float repeats = 1)
        {
            for (int i = 0; i < repeats; i++)
            {
                target.color = startColor;
                yield return new WaitForSeconds(duration);
                target.color = endColor;
                yield return new WaitForSeconds(duration);
            }
        }
        public static IEnumerator ToggleActivationAfterDelay(GameObject target, bool active, float delay)
        {
            yield return new WaitForSeconds(delay);
            target.SetActive(active);
        }
    }
}