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
        [SerializeField] private CanvasGroup canvasGroup = default;
        private bool isActive;

        private void Reset()
        {
            tweenPlayer = GetComponent<TweenPlayerBase>();
            canvasGroup = GetComponent<CanvasGroup>();
        }

        void Awake()
        {
            if (gameObject.activeSelf)
            {
                isActive = true;
            }
            else if (canvasGroup != null)
            {
                canvasGroup.interactable = false;
            }
        }

        private void OnEnable()
        {
            tweenPlayer.OnDisabled += TweenPlayer_OnDisabled;
        }

        private void OnDisable()
        {
            tweenPlayer.OnDisabled -= TweenPlayer_OnDisabled;
        }

        private void TweenPlayer_OnDisabled(TweenPlayerBase obj)
        {
            if (!isActive)
            {
                gameObject.SetActive(false);
            }          
        }

        public void SetState(bool active)
        {
            if (isActive == active)
            {
                return;
            }

            if (canvasGroup != null)
            {
                canvasGroup.interactable = active;
            }
            
            isActive = active;

            if (active)
            {
                gameObject.SetActive(true);
            }
            tweenPlayer.Play(!active);
        }
    }
}