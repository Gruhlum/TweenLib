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

        protected override void Reset()
        {
            base.Reset();
            PlayOnEnable = false;
        }

        protected override void OnDisable()
        {
            base.OnDisable();
            State = false;
            ResetEffects();
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
            Play(!State);
        }
    }
}