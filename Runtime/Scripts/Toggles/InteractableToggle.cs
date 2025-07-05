using HexTecGames.Basics;
using UnityEngine;
using UnityEngine.UI;

namespace HexTecGames.TweenLib
{
    /// <summary>
    /// Script that makes sure that the assigned UI elements are not interactable while an animation plays
    /// </summary>
    [RequireComponent(typeof(TweenPlayerBase))]
    public class InteractableToggle : MonoBehaviour
    {
        public enum ToggleType { CanvasGroup, Selectable }
        public ToggleType toggleType;
        [SerializeField] private TweenPlayerBase tweenPlayer = default;
        [SerializeField, DrawIf(nameof(toggleType), ToggleType.Selectable)] private Selectable selectable = default;
        [SerializeField, DrawIf(nameof(toggleType), ToggleType.CanvasGroup)] private CanvasGroup canvasGroup = default;

        private void Reset()
        {
            tweenPlayer = GetComponent<TweenPlayerBase>();
            selectable = GetComponent<Selectable>();
            canvasGroup = GetComponent<CanvasGroup>();
        }

        private void Awake()
        {
            tweenPlayer.OnStartPlaying += TweenPlayer_OnStartPlaying;
            tweenPlayer.OnDisabled += TweenPlayer_OnDisabled;
        }

        private void OnDestroy()
        {
            tweenPlayer.OnStartPlaying -= TweenPlayer_OnStartPlaying;
            tweenPlayer.OnDisabled -= TweenPlayer_OnDisabled;
        }

        private void ToggleInteractability(bool interactable)
        {
            if (toggleType == ToggleType.Selectable)
            {
                selectable.interactable = interactable;
            }
            else if (toggleType == ToggleType.CanvasGroup)
            {
                canvasGroup.interactable = interactable;
            }
        }

        private void TweenPlayer_OnStartPlaying(TweenPlayerBase obj)
        {
            ToggleInteractability(false);
        }

        private void TweenPlayer_OnDisabled(TweenPlayerBase obj)
        {
            ToggleInteractability(true);
        }
    }
}