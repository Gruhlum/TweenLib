using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace HexTecGames.TweenLib
{
    public class MouseInputToggle : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
    {
        [SerializeField] private ToggleTweenPlayer toggleTweenPlayer = default;
        [SerializeField, Tooltip("(Optional) Won't activate when this is not interactable")] private Selectable selectable = default;

        private void Reset()
        {
            toggleTweenPlayer = GetComponent<ToggleTweenPlayer>();
            selectable = GetComponent<Selectable>();
        }

        public void OnPointerEnter(PointerEventData eventData)
        {
            if (toggleTweenPlayer == null)
            {
                return;
            }
            if (selectable != null && !selectable.interactable)
            {
                return;
            }
            if (!toggleTweenPlayer.enabled)
            {
                return;
            }
            toggleTweenPlayer.SetState(true);
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            if (toggleTweenPlayer == null)
            {
                return;
            }
            if (!toggleTweenPlayer.enabled)
            {
                return;
            }
            toggleTweenPlayer.SetState(false);
        }
    }
}