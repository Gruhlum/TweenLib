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

        //private void OnDisable()
        //{
        //    if (this.enabled)
        //    {
        //        SetAnimationToStart();
        //    }          
        //}

        public void ToggleState()
        {
            SetState(!State);
        }

        public void SetState(bool active)
        {
            //Debug.Log("Set state: " + active);
            if (State == active)
            {
                return;
            }
            State = active;
            Play(!State);
        }
    }
}