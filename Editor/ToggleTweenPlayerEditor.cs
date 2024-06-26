using HexTecGames.TweenLib;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace HexTecGames.TweenLib.Editor
{
    [CustomEditor(typeof(ToggleTweenPlayer))]
    public class ToggleTweenPlayerEditor : TweenPlayerEditor
    {
        private bool hasCompletedCircle;

        protected override void OnDisable()
        {
            hasCompletedCircle = true;
            base.OnDisable();
        }

        protected override void StartAnimation()
        {
            hasCompletedCircle = false;
            base.StartAnimation();
        }

        protected override void StopAnimation()
        {
            if (hasCompletedCircle)
            {
                base.StopAnimation();
            }
            else
            {
                tweenPlayer.PlayReversed();
                hasCompletedCircle = true;
                reachedEnd = false;
            }
        }
    }
}