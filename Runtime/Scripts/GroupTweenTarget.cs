using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace HexTecGames.TweenLib
{
    [System.Serializable]
    public class GroupTweenTarget
    {
        [SubclassSelector, SerializeReference] public TweenData data;
        [Space] public List<Component> targets;

        public bool IsEndless
        {
            get
            {
                if (data == null)
                {
                    return false;
                }
                else return data.EndlessLoop;
            }
        }


        public GroupTweenTarget()
        { }
       
        public void PerformTargetCheck()
        {
            if (data == null)
            {
                return;
            }
            if (targets == null)
            {
                return;
            }
            for (int i = targets.Count - 1; i >= 0; i--)
            {
                if (!data.CheckForCorrectComponent(targets[i]))
                {
                    targets[i] = null;
                    //Debug.Log($"Wrong Type: Component must be of type '{data.GetTargetType()}'");
                }
            }        
        }
        public List<Tween> Create()
        {
            List<Tween> results = new List<Tween>();
            foreach (var target in targets)
            {
                results.Add(data.Create(target));
            }
            return results;
        }
    }
}