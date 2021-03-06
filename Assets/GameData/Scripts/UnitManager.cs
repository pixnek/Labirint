using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Labirint
{
    public class UnitManager : MonoBehaviour
    {
        private static UnitManager instance;

        public delegate void AllyChangeStateAction();
        public static AllyChangeStateAction OnAllyChangeState;
        public delegate void ChangeDistanceEnemyToGamern(float percent);
        public static ChangeDistanceEnemyToGamern OnChangeDistanceEnemyToGamern;

        private static List<IFollower> followers = new List<IFollower>();
        private static List<IFollower> allyFollower = new List<IFollower>();

        private static ITargetFollower currentTargetFollower = null;

        private static bool isEnable = true;
        public static int AllyCount
        {
            get
            {
                return allyFollower.Count;
            }
        }
        public static int FollowingAllyCount
        {
            get
            {
                int countEmpty = 0;
                foreach(var follower in allyFollower)
                {
                    if(follower.GetState() == FollowerState.Following)
                    {
                        countEmpty++;
                    }
                }
                return countEmpty;
            }
        }

        private void Awake()
        {
            if(instance != null)
            {
                Destroy(gameObject);
            }
            else
            {
                instance = this;
            }
        }
        private void Start()
        {
            GameManager.OnGameOver += StopAll;
            GameManager.OnGameFinish += StopAll;
        }
        public static void AddFollower(IFollower inFollower)
        {
            if (!followers.Contains(inFollower))
            {
                followers.Add(inFollower);
                if(currentTargetFollower != null)
                {
                    inFollower.TryGoTo(currentTargetFollower);
                }
                if(inFollower.GetFollowerType() == FollowerType.Ally)
                {
                    if (!allyFollower.Contains(inFollower))
                    {
                        allyFollower.Add(inFollower);
                    }
                }
                OnAllyChangeState?.Invoke();
            }
        }
        public static void ChangeState(IFollower inFollower, FollowerState newState)
        {
            if(inFollower.GetFollowerType() == FollowerType.Ally)
            {
                OnAllyChangeState?.Invoke();
            }
        }
        public static void RemoveFollower(IFollower inFollower)
        {
            if (followers.Remove(inFollower))
            {
                OnAllyChangeState?.Invoke();
            }
        }
        public static void SetTargetFollowing(ITargetFollower inTarget)
        {
            currentTargetFollower = inTarget;
        }
        private void FixedUpdate()
        {
            if (isEnable)
            {
                foreach(var follower in followers)
                {
                    follower.TryGoTo(currentTargetFollower);
                }
                float? percentToGamer = null;
                foreach(var follower in followers)
                {
                    if(follower.GetFollowerType() == FollowerType.Enemy)
                    {
                        if(follower.GetState() == FollowerState.HasTarget)
                        {
                            float itemPercent = follower.DistanceToTarget / follower.MaxDistanceToTarget;
                            if(percentToGamer == null)
                            {
                                percentToGamer = itemPercent;
                            }
                            else
                            {
                                if( percentToGamer.Value > itemPercent)
                                {
                                    percentToGamer = itemPercent;
                                }
                            }
                        }
                    }
                }
                OnChangeDistanceEnemyToGamern?.Invoke(percentToGamer == null ? -1f : percentToGamer.Value);
            }
        }
        public static void StopAll()
        {
            foreach (var follower in followers)
            {
                follower.StopFollow();
            }
            isEnable = false;
        }
        private void OnDestroy()
        {
            GameManager.OnGameOver -= StopAll;
            GameManager.OnGameFinish -= StopAll;
        }
        private void Clear()
        {
            followers.Clear();
        }
    }
}