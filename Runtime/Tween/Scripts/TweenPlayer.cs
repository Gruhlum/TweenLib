using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace HexTecGames.TweenLib
{
    public class TweenPlayer : MonoBehaviour
    {
        [SerializeField] private GameObject go = default;
        public bool StartOnEnabled
        {
            get
            {
                return startOnEnabled;
            }
            set
            {
                startOnEnabled = value;
            }
        }
        [SerializeField] private bool startOnEnabled = true;

        [SerializeReference, SubclassSelector]
        public List<Tween> tweens = new List<Tween>();
        private float timer;

        [SerializeField] private TweenPreset loadPreset = default;

        private void OnValidate()
        {
            if (loadPreset != null)
            {
                tweens.Clear();
                if (loadPreset.Groups.Count > 1)
                {
                    Debug.LogWarning("Preset contains GroupData that will be lost. Use TweenHandler instead.");
                }
                foreach (var group in loadPreset.Groups)
                {
                    foreach (var data in group.datas)
                    {
                        tweens.Add(data.Create());
                    }
                }
                loadPreset = null;
            }
        }

        [ContextMenu("Generate Preset")]
        public void GeneratePreset()
        {
            TweenPreset preset = ScriptableObject.CreateInstance<TweenPreset>();
            preset.Groups.Add(new TweenDataGroup());
            foreach (var tween in tweens)
            {
                preset.Groups[0].datas.Add(tween.GetData());
            }
            AssetDatabase.CreateAsset(preset, "Assets/Test/Preset.asset");
            EditorUtility.SetDirty(preset);
            AssetDatabase.SaveAssets();
            AssetDatabase.Refresh();
        }

        private void Awake()
        {
            if (tweens != null)
            {
                foreach (var tween in tweens)
                {
                    tween.Init(go);
                }
            }
        }

        private void Reset()
        {
            go = gameObject;
        }

        private void Update()
        {
            timer += Time.deltaTime;
            if (tweens != null)
            {
                foreach (var tween in tweens)
                {
                    if (tween != null)
                    {
                        tween.Evaluate(timer);
                    }
                }
            }
        }

        private void OnEnable()
        {
            timer = 0;
            if (startOnEnabled)
            {
                tweens.ForEach(x => x.IsEnabled = true);
            }
        }
    }
}