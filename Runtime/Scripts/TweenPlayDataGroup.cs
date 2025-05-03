using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace HexTecGames.TweenLib
{
    [System.Serializable]
    public class TweenPlayDataGroup
    {
        public bool IsPlaying
        {
            get
            {
                return isPlaying;
            }
            private set
            {
                isPlaying = value;
            }
        }
        [SerializeField] private bool isPlaying;
        public bool IsEndless
        {
            get
            {
                return isEndless;
            }
            private set
            {
                isEndless = value;
            }
        }
        private bool isEndless;

        private bool reverse;

        private List<TweenPlayData> tweenPlayDatas;

        private TweenPlayData currentData;
        private int dataIndex = -1;

        public TweenPlayDataGroup(TweenPlayData tweenPlayData) : this(new List<TweenPlayData>() { tweenPlayData })
        {
        }
        public TweenPlayDataGroup(List<Tween> tweens) : this(new TweenPlayData(tweens))
        {
        }
        public TweenPlayDataGroup(List<TweenPlayData> tweenPlayDatas)
        {
            this.tweenPlayDatas = tweenPlayDatas;
        }

        public void Start(bool reverse)
        {
            this.reverse = reverse;

            if (!IsPlaying)
            {
                if (reverse)
                {
                    dataIndex = tweenPlayDatas.Count;
                }

                TrySetupNextData();
                IsPlaying = true;
            }
            else currentData.Start(reverse);
        }
        private void TrySetupNextData()
        {
            currentData = GetNextPlayData();
            if (currentData == null)
            {
                ReachedEnd();
            }
            else currentData.Start(reverse);
        }

        public void MoveToEnd()
        {
            foreach (var data in tweenPlayDatas)
            {
                data.MoveToEnd();
            }
        }
        public void MoveToStart()
        {
            foreach (var data in tweenPlayDatas)
            {
                data.MoveToStart();
            }
        }
        private void ReachedEnd()
        {
            IsPlaying = false;
            dataIndex = -1;
        }

        public void Stop()
        {
            ReachedEnd();
            if (currentData != null)
            {
                currentData.Stop();
            }
        }
        private TweenPlayData GetNextPlayData()
        {
            int change = reverse ? -1 : 1;
            if (change + dataIndex < 0)
            {
                return null;
            }
            if (change + dataIndex >= tweenPlayDatas.Count)
            {
                return null;
            }

            dataIndex = dataIndex.WrapIndex(change, tweenPlayDatas.Count);
            return tweenPlayDatas[dataIndex];
        }
        public void Evaluate(float timeStep)
        {
            if (!IsPlaying || currentData == null)
            {
                return;
            }
            bool finished = currentData.Evaluate(timeStep);
            if (finished)
            {
                TrySetupNextData();
            }
        }
        public void AddDelay(float amount, Position pos)
        {

        }
        public void ResetStartDatas()
        {
            foreach (var tweenData in tweenPlayDatas)
            {
                tweenData.ResetStartDatas();
            }
        }
        public void ResetEffect()
        {
            for (int i = 0; i < tweenPlayDatas.Count; i++)
            {
                tweenPlayDatas[i].ResetEffect();
            }
        }
    }
}