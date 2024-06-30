using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace HexTecGames.TweenLib
{
    public class TweenPlayer : TweenPlayerBase
    {
        [SerializeField] private GameObject target;

        protected void Reset()
        {
            target = this.gameObject;
        }

        protected override List<GameObject> GetTargetGameObjects()
        {
            return new List<GameObject>() { target };
        }
    }
}