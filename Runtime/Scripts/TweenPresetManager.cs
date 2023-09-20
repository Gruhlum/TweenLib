using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEditor;
using UnityEngine;

namespace HexTecGames.TweenLib
{
    public class TweenPresetManager : MonoBehaviour
    {
        public TweenPlayer TweenPlayer
        {
            get
            {
                return this.tweenPlayer;
            }
            private set
            {
                this.tweenPlayer = value;
            }
        }

        [SerializeField] private TweenPlayer tweenPlayer = default;
        [SerializeField] private string folderLocation = "Assets/ScriptableObjects/TweenPresets";
        [SerializeField] private string presetName = "Preset";

        private void Reset()
        {
            TweenPlayer = GetComponent<TweenPlayer>();
        }

        //[ContextMenu("Load Preset")]
        //public void LoadPreset()
        //{
        //    if (tweenPlayer == null)
        //    {
        //        return;
        //    }
        //    if (presetToLoad == null)
        //    {
        //        return;
        //    }
        //    tweenPlayer.LoadTweens(presetToLoad.TweenDatas);
        //}

        [ContextMenu("Generate Preset")]
        public void GeneratePreset()
        {
#if (UNITY_EDITOR)
            if (TweenPlayer == null)
            {
                return;
            }
            TweenPreset preset = ScriptableObject.CreateInstance<TweenPreset>();
            preset.name = presetName;
            preset.TweenDatas = TweenPlayer.GetTweenData();
            AssetDatabase.CreateAsset(preset, Path.Combine(folderLocation, presetName) + ".asset");
            EditorUtility.SetDirty(preset);
            AssetDatabase.SaveAssets();
            AssetDatabase.Refresh();
#endif
        }
    }
}