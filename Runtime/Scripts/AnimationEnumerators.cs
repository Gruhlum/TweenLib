using System;
using System.Collections;
using System.Collections.Generic;
using HexTecGames.EaseFunctions;
using UnityEngine;
using UnityEngine.UI;

namespace HexTecGames.TweenLib
{
    public static class AnimationEnumerators
    {
        public static IEnumerator Fade(this CanvasGroup canvasGroup, float duration, float start, float end, EaseFunction easing)
        {
            float timer = 0;
            while (timer < duration)
            {
                timer += Time.deltaTime;
                canvasGroup.alpha = Mathf.Lerp(start, end, easing.GetValue(timer / duration));
                yield return null;
            }
        }
        public static IEnumerator Fade(this CanvasGroup canvasGroup, float duration, float start, float end)
        {
            float timer = 0;
            while (timer < duration)
            {
                timer += Time.deltaTime;
                canvasGroup.alpha = Mathf.Lerp(start, end, timer / duration);
                yield return null;
            }
        }

        public static IEnumerator LerpColor(this Image target, float duration, Color start, Color end)
        {
            float timer = 0;
            while (timer < duration)
            {
                timer += Time.deltaTime;
                target.color = Color.Lerp(start, end, timer / duration);
                yield return null;
            }
        }
        public static IEnumerator LerpColor(this SpriteRenderer target, float duration, Color start, Color end)
        {
            float timer = 0;
            while (timer < duration)
            {
                timer += Time.deltaTime;
                target.color = Color.Lerp(start, end, timer / duration);
                yield return null;
            }
        }

        public static IEnumerator LerpColor(this Image target, float duration, Color start, Color end, EaseFunction easing)
        {
            float timer = 0;
            while (timer < duration)
            {
                timer += Time.deltaTime;
                target.color = Color.Lerp(start, end, easing.GetValue(timer / duration));
                yield return null;
            }
        }
        public static IEnumerator LerpColor(this SpriteRenderer target, float duration, Color start, Color end, EaseFunction easing)
        {
            float timer = 0;
            while (timer < duration)
            {
                timer += Time.deltaTime;
                target.color = Color.Lerp(start, end, easing.GetValue(timer / duration));
                yield return null;
            }
        }

        public static IEnumerator Fade(this Image target, float duration, float startAlpha, float endAlpha)
        {
            yield return LerpColor(target, duration, target.color.GetColorWithAlpha(startAlpha), target.color.GetColorWithAlpha(endAlpha));
        }
        public static IEnumerator Fade(this SpriteRenderer target, float duration, float startAlpha, float endAlpha)
        {
            yield return LerpColor(target, duration, target.color.GetColorWithAlpha(startAlpha), target.color.GetColorWithAlpha(endAlpha));
        }


        public static IEnumerator Fade(this Image target, float duration, float startAlpha, float endAlpha, EaseFunction easing)
        {
            yield return LerpColor(target, duration, target.color.GetColorWithAlpha(startAlpha), target.color.GetColorWithAlpha(endAlpha), easing);
        }
        public static IEnumerator Fade(this SpriteRenderer target, float duration, float startAlpha, float endAlpha, EaseFunction easing)
        {
            yield return LerpColor(target, duration, target.color.GetColorWithAlpha(startAlpha), target.color.GetColorWithAlpha(endAlpha), easing);
        }


        public static IEnumerator FlashColor(this SpriteRenderer target, float duration, Color startColor, Color endColor, float repeats = 1)
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