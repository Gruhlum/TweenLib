using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SocialPlatforms;

namespace HexTecGames.TweenLib
{
	public class ToggleTweenPlayer : TweenPlayer
    {       
        public bool State
        {
            get
            {
                return this.state;
            }
            private set
            {
                this.state = value;
            }
        }
        private bool state;


        private void OnDisable()
        {
            SetState(false);
        }

        public void ToggleState()
        {
            SetState(!State);
        }

        public void SetState(bool active)
        {
            if (State == active)
            {
                return;
            }
            State = active;
            TimeScale = active ? 1 : -1;
            if (!IsPlaying)
            {
                Play();
            }
        }
    }
}