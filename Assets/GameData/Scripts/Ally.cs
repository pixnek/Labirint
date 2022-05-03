using UnityEngine;

namespace Labirint
{
    public class Ally : Follower
    {
        private void OnTriggerEnter(Collider other)
        {
            if(CurrentState != FollowerState.Following)
            {
                GamerController gamer = other.GetComponent<GamerController>();
                if(gamer != null)
                {
                    Debug.Log("Ally touch");
                    CurrentState = FollowerState.Following;
                }
            }
        }
        public override bool TryGoTo(ITargetFollower Target)
        {
            if(CurrentState != FollowerState.Following)
            {
                base.TryGoTo(Target);
            }
            return true;
        }
        private void FixedUpdate()
        {
            if(CurrentState == FollowerState.Following)
            {
                currentAgent.destination = target.CurrentGameObject.transform.position;
            }
        }
    }
}