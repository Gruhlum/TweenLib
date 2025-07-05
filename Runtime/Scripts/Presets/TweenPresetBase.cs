using UnityEngine;

namespace HexTecGames.TweenLib
{
    public abstract class TweenPresetBase : ScriptableObject
    {
        public abstract TweenPlayDataGroup GenerateTweenPlayData(GameObject go);
    }
}