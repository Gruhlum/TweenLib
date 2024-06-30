using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace HexTecGames.TweenLib
{
    public class TweenGroupPlayer : TweenPlayerBase
    {
        [SerializeField] private List<GameObject> targetGOs = default;

        protected override List<GameObject> GetTargetGameObjects()
        {
            return targetGOs;
        }
    }
}