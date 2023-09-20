using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

namespace HexTecGames.TweenLib
{
    public class MouseInputTweenPlayer : ToggleTweenPlayer, IPointerEnterHandler, IPointerExitHandler
    {       
        public void OnPointerEnter(PointerEventData eventData)
        {
            SetState(true);
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            SetState(false);
        }
    }
}