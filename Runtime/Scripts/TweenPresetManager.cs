using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEditor;
using UnityEngine;

namespace HexTecGames.TweenLib
{
    public class TweenPresetManager : MonoBehaviour
    {
        [SerializeField] private TweenPlayer tweenPlayer = default;
        [SerializeField] private string folderLocation = "Assets/Scriptables/TweenPresets";
        [SerializeField] private string presetName = "Preset";
        [SerializeField] private TweenPreset presetToLoad = default;

        private void Reset()
        {
            tweenPlayer = GetComponent<TweenPlayer>();
        }

        [ContextMenu("Load Preset")]
        public void LoadPreset()
        {
            if (tweenPlayer == null)
            {
                return;
            }
            if (presetToLoad == null)
            {
                return;
            }
            tweenPlayer.LoadTweens(presetToLoad.TweenDatas);
        }

        [ContextMenu("Generate Preset")]
        public void GeneratePreset()
        {
            if (tweenPlayer == null)
            {
                return;
            }
            TweenPreset preset = ScriptableObject.CreateInstance<TweenPreset>();
            preset.name = presetName;
            preset.TweenDatas = tweenPlayer.GetTweenData();
            AssetDatabase.CreateAsset(preset, Path.Combine(folderLocation, presetName) + ".asset");
            EditorUtility.SetDirty(preset);
            AssetDatabase.SaveAssets();
            AssetDatabase.Refresh();
        }
    }
}