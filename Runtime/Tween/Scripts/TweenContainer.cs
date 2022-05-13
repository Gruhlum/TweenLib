using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;

namespace HexTecGames.TweenLib
{
    [System.Serializable]
    public class TweenContainer
    {       
        public GameObject GameObject
        {
            get
            {
                return gameObject;
            }
            set
            {
                gameObject = value;
            }
        }
        [Tooltip("Affected GameObject")]
        [SerializeField] private GameObject gameObject;
        public Tween Tween
        {
            get
            {
                return tween;
            }
            private set
            {
                tween = value;
            }
        }
        private Tween tween;


        public void GenerateTweens(in List<Tween> tweens)
        {
            //Tween = TweenData.Construct(GameObject);
            tweens.Add(Tween);
        }
    }
}