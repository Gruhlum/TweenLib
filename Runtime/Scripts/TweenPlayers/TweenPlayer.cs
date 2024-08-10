using HexTecGames.Basics;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace HexTecGames.TweenLib
{
    public class TweenPlayer : TweenPlayerBase
    {
        [SerializeField] private List<TweenTarget> tweenDatas = default;

        private void OnValidate()
        {
            foreach (var animation in tweenDatas)
            {
                if (animation != null)
                {
                    animation.FindCorrectTarget(this.gameObject);
                    animation.PerformTargetCheck();
                }
            }
        }

        //protected List<TweenPlayDataGroup> GenerateSpecificTweenPlayDatas()
        //{
        //    List<TweenPlayDataGroup> results = new List<TweenPlayDataGroup>();
        //    foreach (var tweenItem in tweenDatas)
        //    {
        //        //results.AddRange(tweenItem.GenerateTweenPlayData());
        //    }
        //    return results;
        //}
        protected virtual void Reset()
        {
            tweenDatas = new List<TweenTarget>
            {
                new TweenTarget()
            };
            //animationTargets = new List<GroupTweenTarget>();
            //GroupTweenTarget groupTweenTarget = new GroupTweenTarget();
            //groupTweenTarget.targetGOs.Add(this.gameObject);
            //groupTweenTarget.animations.Add(null);
            //animationTargets.Add(groupTweenTarget);
        }

        protected override List<TweenPlayDataGroup> GenerateTweenPlayDatas()
        {
            List<TweenPlayDataGroup> results = new List<TweenPlayDataGroup>();
            List<Tween> tweens = new List<Tween>();
            foreach (var anim in tweenDatas)
            {
                if (anim == null)
                {
                    continue;
                }
                Tween tween = anim.Create();
                tweens.Add(tween);
            }
            results.Add(new TweenPlayDataGroup(tweens));
            return results;
        }
    }
}