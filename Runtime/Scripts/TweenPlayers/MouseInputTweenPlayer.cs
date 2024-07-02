using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace HexTecGames.TweenLib
{
    [RequireComponent(typeof(ToggleTweenPlayer))]
    public class MouseInputTweenPlayer : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
    {
        [SerializeField] private ToggleTweenPlayer toggleTweenPlayer = default;
        [SerializeField] private Selectable selectable = default;

        private void Reset()
        {
            toggleTweenPlayer = GetComponent<ToggleTweenPlayer>();
            selectable = GetComponent<Selectable>();
        }

        public void OnPointerEnter(PointerEventData eventData)
        {
            if (selectable != null && !selectable.interactable)
            {
                return;
            }
            toggleTweenPlayer.SetState(true);
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            toggleTweenPlayer.SetState(false);
        }
    }
}