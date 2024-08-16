using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace HexTecGames.TweenLib
{
    public class SequenceTweenPlayer : MonoBehaviour
    {
        [SerializeField] private List<TweenPlayerBase> tweenPlayers = default;

        [ContextMenu("Play")]
        public void Play()
        {
            Play(false);
        }
        public void Play(bool reverse)
        {
            StartCoroutine(PlayTweensInSequence(tweenPlayers, reverse));
        }
        private IEnumerator PlayTweensInSequence(List<TweenPlayerBase> tweenPlayers, bool reverse)
        {
            foreach (var tweenPlayer in tweenPlayers)
            {
                yield return tweenPlayer.PlayCoroutine(reverse);
            }
        }
    }
}