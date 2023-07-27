using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace HexTecGames.TweenLib
{
	public class TweenPresetManager : MonoBehaviour
	{
        [SerializeField] private TweenPlayer tweenPlayer = default;
        [SerializeField] private string folderLocation = "Assets/Test/";
        [SerializeField] private string  presetName = "Preset";
        [SerializeField] private TweenPreset presetToLoad = default;

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
            tweenPlayer.tweens.Clear();
            if (presetToLoad.Groups.Count > 1)
            {
                Debug.LogWarning("Preset contains GroupData that will be lost. Use TweenHandler instead.");
            }
            foreach (var group in presetToLoad.Groups)
            {
                foreach (var data in group.datas)
                {
                    tweenPlayer.tweens.Add(data.Create());
                }
            }
        }

        [ContextMenu("Generate Preset")]
        public void GeneratePreset()
        {
            if (tweenPlayer == null)
            {
                return;
            }
            TweenPreset preset = ScriptableObject.CreateInstance<TweenPreset>();
            preset.Groups.Add(new TweenDataGroup());
            foreach (var tween in tweenPlayer.tweens)
            {
                preset.Groups[0].datas.Add(tween.GetData());
            }
            AssetDatabase.CreateAsset(preset, folderLocation + presetName + ".asset");
            EditorUtility.SetDirty(preset);
            AssetDatabase.SaveAssets();
            AssetDatabase.Refresh();
        }
    }
}