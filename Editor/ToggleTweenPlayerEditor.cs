using HexTecGames.TweenLib;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace HexTecGames.TweenLib.Editor
{
    [CustomEditor(typeof(ToggleTweenPlayer))]
    public class ToggleTweenPlayerEditor : TweenPlayerEditor
    {
        public override void OnInspectorGUI()
        {
            ToggleTweenPlayer tweenPlayer = (ToggleTweenPlayer)target;

            if (Application.isPlaying && GUILayout.Button("Toggle", GUILayout.Width(180), GUILayout.Height(30)))
            {
                tweenPlayer.ToggleState();
            }
            DrawDefaultInspector();
        }
    }
}