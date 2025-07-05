using System.Collections.Generic;
using UnityEngine;

namespace HexTecGames.TweenLib
{
    //[CreateAssetMenu(menuName = "HexTecGames/TweenLib/Animation"), CanEditMultipleObjects]
    public class TweenPreset : TweenPresetBase
    {
        [SerializeReference, SubclassSelector] protected TweenData tweenData = null;

        public override TweenPlayDataGroup GenerateTweenPlayData(GameObject go)
        {
            return new TweenPlayDataGroup(new TweenPlayData(GenerateTweens(go)));
        }
        public List<Tween> GenerateTweens(GameObject go)
        {
            List<Tween> results = new List<Tween>();
            Tween t = null;// tweenData.Create();
            results.Add(t);
            return results;
        }
    }
}