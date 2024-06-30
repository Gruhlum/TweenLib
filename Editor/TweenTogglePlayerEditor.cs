using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace HexTecGames.TweenLib.Editor
{
    [CustomEditor(typeof(ToggleTweenPlayer), true)]
    public class TweenTogglePlayerEditor : TweenPlayerEditor
    {
        private int multiplier = 1;
        private bool isExpanded;

        public override void OnInspectorGUI()
        {
            tweenPlayer = (TweenPlayer)target;

            if (isExpanded || Application.isPlaying)
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
            DrawDefaultInspector();
        }
        protected override void EndTimeReached()
        {
            StopAnimation(false);
            reachedEnd = false;
        }
    }
}