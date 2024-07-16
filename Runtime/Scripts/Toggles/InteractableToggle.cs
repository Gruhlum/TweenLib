using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace HexTecGames.TweenLib
{
    [RequireComponent(typeof(TweenPlayerBase))]
    public class InteractableToggle : MonoBehaviour
	{
		[SerializeField] private TweenPlayerBase tweenPlayer = default;
        [SerializeField] private Selectable selectable = default;

        private void Reset()
        {
            tweenPlayer = GetComponent<TweenPlayerBase>();
            selectable = GetComponent<Selectable>();
        }

        private void OnEnable()
        {
            tweenPlayer.OnDisabled += TweenPlayer_OnDisabled;

            if (selectable != null)
            {
                selectable.interactable = false;
            }
        }

        private void OnDisable()
        {
            tweenPlayer.OnDisabled -= TweenPlayer_OnDisabled;
        }

        private void TweenPlayer_OnDisabled(TweenPlayerBase obj)
        {
            selectable.interactable = true;
        }
    }
}