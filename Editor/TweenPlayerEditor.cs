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

        protected float endDelay = 0.5f;
        protected float endTimer = 0;
        protected bool reachedEnd;
        protected bool isPlaying;
        private bool hasPlayed;


        //public override VisualElement CreateInspectorGUI()
        //{
        //    TweenPlayer tweenPlayer = (TweenPlayer)target;

        //    var root = new VisualElement();

        //    var foldout = new Foldout();

        //    //if (tweenPlayer.animations.Count > 0)
        //    //{
        //    //    InspectorElement animationElement = new InspectorElement();
        //    //    InspectorElement.FillDefaultInspector(animationElement, new SerializedObject(tweenPlayer.animations[0]), this);
        //    //    foldout.Add(animationElement);
        //    //}

        //    InspectorElement.FillDefaultInspector(foldout, serializedObject, this);
        //    root.Add(foldout);
            
        //    return root;
        //}

        public override void OnInspectorGUI()
        {
            if (EditorUtility.IsPersistent(target))
            {
                DrawDefaultInspector();
                return;
            }

            tweenPlayer = (TweenPlayerBase)target;

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