using UnityEngine;


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
        [SerializeField] private bool state;

        public override bool ResetStartDataOnPlay
        {
            get
            {
                if (State != startState)
                {
                    return base.ResetStartDataOnPlay;
                }
                else return false;
            }
            set
            {
                base.ResetStartDataOnPlay = value;
            }
        }

        private bool startState;

        protected override void Reset()
        {
            base.Reset();
            PlayOnEnable = false;
        }

        protected override void OnDisable()
        {
            base.OnDisable();
            State = false;
        }
        protected override void OnEnable()
        {
            startState = State;

            if (!tweensAreInitialized)
            {
                InitTweens();
            }
            if (State == true)
            {
                foreach (TweenPlayDataGroup tween in tweenPlayDatas)
                {
                    tween.MoveToEnd();
                }
            }
            base.OnEnable();
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