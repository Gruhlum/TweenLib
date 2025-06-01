using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace HexTecGames.TweenLib.Editor
{
    [CustomEditor(typeof(ToggleTweenPlayer), true), CanEditMultipleObjects]
    public class TweenTogglePlayerEditor : TweenPlayerEditor
    {
        private bool isExpanded;


        public override void OnInspectorGUI()
        {
            if (EditorUtility.IsPersistent(target))
            {
                DrawDefaultInspector();
                return;
            }

            ToggleTweenPlayer tweenPlayer = (ToggleTweenPlayer)target;

            if (Application.isPlaying)
            {
                if (tweenPlayer.State)
                {
                    if (CreateButton("Toggle Off"))
                    {
                        tweenPlayer.ToggleState();
                    }
                }
                else
                {
                    if (CreateButton("Toggle On"))
                    {
                        tweenPlayer.ToggleState();
                    }

                }
            }
            else
            {
                if (isExpanded)
                {
                    if (CreateButton("Toggle Off"))
                    {
                        StartAnimation(true, false);
                        isExpanded = false;
                    }
                }
                else
                {
                    if (CreateButton("Toggle On"))
                    {
                        StartAnimation(false, !(isExpanded || isPlaying));
                        isExpanded = true;
                    }
                }
            }
            
            DrawDefaultInspector();
        }

        protected override void InitTweens()
        {
            base.InitTweens();
        }
        protected override void EndTimeReached()
        {
            StopAnimation(false);
            reachedEnd = false;
        }
    }
}