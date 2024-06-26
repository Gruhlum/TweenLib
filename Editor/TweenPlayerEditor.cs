using HexTecGames.TweenLib;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace HexTecGames.TweenLib.Editor
{
    [CustomEditor(typeof(TweenPlayer))]
    public class TweenPlayerEditor : UnityEditor.Editor
    {
        protected TweenPlayer tweenPlayer;

        private float endDelay = 0.5f;
        private float endTimer = 0;
        protected bool reachedEnd;
        protected bool isPlaying;

        protected virtual void OnDisable()
        {
            if (!EditorApplication.isPlaying && isPlaying)
            {
                StopAnimation();
            }           
        }

        public override void OnInspectorGUI()
        {
            tweenPlayer = (TweenPlayer)target;

            if (isPlaying)
            {
                if (tweenPlayer.IsLooping)
                {
                    if (CreateButton("Stop"))
                    {
                        StopAnimation();
                    }
                }
                else
                {
                    if (CreateButton("Play"))
                    {
                        StopAnimation();
                        StartAnimation();
                    }
                }                
            }
            else
            {
                if (CreateButton("Play"))
                {
                    StartAnimation();
                }
            }
            DrawDefaultInspector();
        }
        private bool CreateButton(string text)
        {
            return GUILayout.Button(text, GUILayout.ExpandWidth(true), GUILayout.Height(30));
        }
        protected virtual void StartAnimation()
        {
            isPlaying = true;
            tweenPlayer.InitTweens();
            tweenPlayer.Play();

            if (!EditorApplication.isPlaying)
            {
                EditorApplication.update += AdvanceTime;
                tweenPlayer.FinishedPlaying += TweenPlayer_FinishedPlaying;
            }
        }
        private void TweenPlayer_FinishedPlaying()
        {
            reachedEnd = true;
        }

        private void AdvanceTime()
        {
            if (reachedEnd)
            {
                endTimer += Time.deltaTime;
                if (endTimer >= endDelay)
                {
                    StopAnimation();
                }
            }
            else tweenPlayer.AdvanceTime(Mathf.Min(Time.deltaTime, Time.fixedDeltaTime));
        }

        protected virtual void StopAnimation()
        {
            isPlaying = false;
            reachedEnd = false;
            endTimer = 0;
            EditorApplication.update -= AdvanceTime;
            tweenPlayer.FinishedPlaying -= TweenPlayer_FinishedPlaying;
            tweenPlayer.ReverseEffect();
        }
    }
}