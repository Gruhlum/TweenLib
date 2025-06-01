using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEditor.UIElements;
using UnityEngine;
using UnityEngine.UIElements;

namespace HexTecGames.TweenLib.Editor
{
    [CustomEditor(typeof(TweenPlayerBase), true), CanEditMultipleObjects]
    public class TweenPlayerEditor : UnityEditor.Editor
    {
        protected TweenPlayerBase tweenPlayer;

        DateTime startTime;
        protected float endDelay = 1f;
        protected float endTimer = 0;
        protected bool reachedEnd;
        protected bool isPlaying;
        private bool hasPlayed;
        DateTime lastFrame;



        private void OnEnable()
        {
            tweenPlayer = (TweenPlayerBase)target;
        }

        public override void OnInspectorGUI()
        {
            if (EditorUtility.IsPersistent(target))
            {
                DrawDefaultInspector();
                return;
            }

            if (isPlaying || Application.isPlaying)
            {
                if (tweenPlayer.IsEndless)
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
                        StartAnimation(false, true);
                    }
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
            lastFrame = DateTime.Now;
            isPlaying = true;
            if (initTweens)
            {
                InitTweens();
            }
            
            tweenPlayer.Play(reversed);
        }
        protected virtual void InitTweens()
        {
            tweenPlayer.InitTweens();
        }
        public void AdvanceTime()
        {
            float deltaTime = ((DateTime.Now - lastFrame).Milliseconds / 1000f);
            lastFrame = DateTime.Now;
            if (reachedEnd)
            {
                if (DateTime.Now > startTime)
                {
                    EndTimeReached();
                }
            }
            else
            {
                if (tweenPlayer.AdvanceTime(deltaTime))
                {
                    reachedEnd = true;
                    startTime = DateTime.Now + TimeSpan.FromSeconds(endDelay);
                }
            }
        }
        protected virtual void EndTimeReached()
        {
            StopAnimation();
            reachedEnd = false;
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