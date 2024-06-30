using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace HexTecGames.TweenLib.Editor
{
    [CustomEditor(typeof(TweenPlayerBase), true)]
    public class TweenPlayerEditor : UnityEditor.Editor
    {
        protected TweenPlayer tweenPlayer;

        protected float endDelay = 0.5f;
        protected float endTimer = 0;
        protected bool reachedEnd;
        protected bool isPlaying;
        private bool hasPlayed;

        public override void OnInspectorGUI()
        {
            tweenPlayer = (TweenPlayer)target;

            if (isPlaying || Application.isPlaying)
            {
                if (CreateButton("Play"))
                {
                    StopAnimation();
                    StartAnimation(false, true);
                }
            }
            else
            {
                if (CreateButton("Play"))
                {
                    StartAnimation(false, true);
                }
            }
            DrawDefaultInspector();
        }

        protected virtual void OnDisable()
        {
            if (hasPlayed && !EditorApplication.isPlaying)
            {
                StopAnimation();
            }
        }

        protected bool CreateButton(string text)
        {
            return GUILayout.Button(text, GUILayout.ExpandWidth(true), GUILayout.Height(30));
        }
        protected virtual void StartAnimation(bool reversed, bool initTweens)
        {
            hasPlayed = true;
            reachedEnd = false;
            endTimer = 0;
            if (!isPlaying && !EditorApplication.isPlaying)
            {
                EditorApplication.update += AdvanceTime;
            }
            isPlaying = true;
            if (initTweens)
            {
                tweenPlayer.InitTweens();
            }
            
            tweenPlayer.Play(reversed);
        }

        protected void AdvanceTime()
        {
            if (reachedEnd)
            {
                endTimer += Time.deltaTime;
                if (endTimer >= endDelay)
                {
                    EndTimeReached();
                }
            }
            else
            {
                if (tweenPlayer.AdvanceTime(CalculateTimeStep()))
                {
                    reachedEnd = true;
                }
            }
        }
        protected virtual void EndTimeReached()
        {
            StopAnimation();
            reachedEnd = false;
        }
        protected virtual float CalculateTimeStep()
        {
            return Mathf.Min(Time.deltaTime, Time.fixedDeltaTime);
        }
        protected virtual void StopAnimation(bool resetEffects = true)
        {
            ResetValues();
            EditorApplication.update -= AdvanceTime;
            if (resetEffects)
            {
                tweenPlayer.ResetEffects();
            }           
        }
        protected void ResetValues()
        {
            isPlaying = false;
            reachedEnd = false;
            endTimer = 0;
            
        }
    }
}