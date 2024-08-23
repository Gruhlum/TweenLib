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

        public void SetTargetGameObject(int index, GameObject gameObject)
        {
            if (index < 0)
            {
                Debug.Log("Negative Index!");
                return;
            }
            if (tweenDatas.Count <= index)
            {
                Debug.Log("Index out of range! Invalid tweenData!");
                return;
            }
            tweenDatas[index].FindCorrectTarget(gameObject);
            InitTweens();
        }
        protected override List<TweenPlayDataGroup> GenerateTweenPlayDatas()
        {
            List<TweenPlayDataGroup> results = new List<TweenPlayDataGroup>();
            List<Tween> tweens = new List<Tween>();
            foreach (var tweenTarget in tweenDatas)
            {
                if (tweenTarget == null)
                {
                    continue;
                }
                Tween tween = tweenTarget.Create();
                tweens.Add(tween);
            }
            results.Add(new TweenPlayDataGroup(tweens));
            return results;
        }
    }
}