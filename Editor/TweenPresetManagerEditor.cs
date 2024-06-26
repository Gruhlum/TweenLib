using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace HexTecGames.TweenLib.Editor
{
    [CustomEditor(typeof(TweenPresetManager))]
    public class TweenPresetManagerEditor : UnityEditor.Editor
    {
        public override void OnInspectorGUI()
        {
            TweenPresetManager presetManager = (TweenPresetManager)target;

            if (presetManager.TweenPlayer != null && GUILayout.Button("Generate Preset", GUILayout.ExpandWidth(true), GUILayout.Height(30)))
            {
                presetManager.GeneratePreset();
            }
            DrawDefaultInspector();
        }
    }
}