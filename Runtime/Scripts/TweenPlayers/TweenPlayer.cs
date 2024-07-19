using HexTecGames.Basics;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace HexTecGames.TweenLib
{
    public class TweenPlayer : TweenPlayerBase
    {
        //[SerializeField] public List<TweenInfo> animations;      
        //[SerializeField] private GameObject target;
        //public enum TargetMode { Single, Group}
        //public TargetMode targetMode;
        //[SerializeField, DrawIf(nameof(targetMode), false)] private List<SingleTweenTarget> singleTargets = default;
        [SerializeField] private List<GroupTweenTarget> animationTargets = default;

        protected List<TweenPlayData> GenerateSpecificTweenPlayDatas()
        {
            List<TweenPlayData> results = new List<TweenPlayData>();
            foreach (var tweenItem in animationTargets)
            {
                results.AddRange(tweenItem.GenerateTweenPlayData());
            }
            return results;
        }
        protected void Reset()
        {
            //target = this.gameObject;
            animationTargets = new List<GroupTweenTarget>();
            GroupTweenTarget groupTweenTarget = new GroupTweenTarget();
            groupTweenTarget.targetGOs.Add(this.gameObject);
            groupTweenTarget.animations.Add(null);
            animationTargets.Add(groupTweenTarget);
        }

        protected override List<TweenPlayData> GenerateTweenPlayDatas()
        {
            List<TweenPlayData> results = new List<TweenPlayData>();
            foreach (var anim in animationTargets)
            {
                results.AddRange(anim.GenerateTweenPlayData());
            }
            return results;
        }
    }
}