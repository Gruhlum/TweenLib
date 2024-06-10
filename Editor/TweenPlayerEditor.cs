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
        public override void OnInspectorGUI()
        {
            TweenPlayer tweenPlayer = (TweenPlayer)target;           
            
            if (Application.isPlaying && GUILayout.Button("Play", GUILayout.Width(180), GUILayout.Height(30)))
            {
                tweenPlayer.UpdateTweenData();
                tweenPlayer.Play();
            }
            DrawDefaultInspector();
        }
    }
}