using UnityEngine;
using UnityEngine.AI;

namespace Labirint
{
    public class Follower : MonoBehaviour, IFollower
    {
        [SerializeField] protected FollowerType type;
        [SerializeField] protected NavMeshAgent currentAgent;
        [SerializeField] private bool isPatrul;
        [SerializeField] private GameObject patrulPoint1;
        [SerializeField] private GameObject patrulPoint2;
        [SerializeField] private float patrulPointDistance;
        
        protected FollowerState currentState;
        public FollowerState CurrentState
        {
            get => currentState;
            set
            {
                if(currentState != value)
                {
                    currentState = value;
                    UnitManager.ChangeState(this, currentState);
                }
            }
        }
        protected ITargetFollower target;

        private bool to1Point = true;
        private ITargetFollower patrulPoint1Target;
        private ITargetFollower patrulPoint2Target;

        private void Start()
        {
            if(patrulPoint1 != null)
            {
                patrulPoint1Target = patrulPoint1.GetComponent<ITargetFollower>();
            }
            if(patrulPoint2 != null)
            {
                patrulPoint2Target = patrulPoint2.GetComponent<ITargetFollower>();
            }
            UnitManager.AddFollower(this);
            if(CurrentState == FollowerState.Empty)
            {
                if (isPatrul)
                {
                    if(patrulPoint1Target == null || patrulPoint2Target == null) 
                    {
                        Debug.LogError("patrul point has contains \"ITargetFollower\"");
                    }
                    else
                    {
                        target = patrulPoint1Target;
                    }
                }
            }
        }
        public FollowerType GetFollowerType()
        {
            return type;
        }
        public FollowerState GetState()
        {
            return CurrentState;
        }
        public virtual bool TryGoTo(ITargetFollower Target)
        {
            RaycastHit hit;
            if (Physics.Raycast(transform.position, Target.CurrentGameObject.transform.position - transform.position, out hit, 100f))
            {
                if(hit.transform.gameObject == Target.CurrentGameObject)
                {
                    to1Point = true;
                    target = Target;
                    CurrentState = FollowerState.HasTarget;
                    currentAgent.destination = Target.CurrentGameObject.transform.position;
                    currentAgent.isStopped = false;
                    return true;
                }
                else
                {
                    CurrentState = FollowerState.Empty;
                    currentAgent.isStopped = true;
                    Patrul();
                    return false;
                }
            }
            return false;
        }
        protected virtual void Patrul()
        {
            if (CurrentState == FollowerState.Empty)
            {
                if (isPatrul)
                {
                    if (to1Point)
                    {
                        target = patrulPoint1Target;
                        if (Vector3.Distance(transform.position, target.CurrentGameObject.transform.position) <= patrulPointDistance)
                        {
                            to1Point = false;
                            target = patrulPoint2Target;
                        }
                    }
                    else
                    {
                        target = patrulPoint2Target;
                        if (Vector3.Distance(transform.position, target.CurrentGameObject.transform.position) <= patrulPointDistance)
                        {
                            to1Point = true;
                            target = patrulPoint1Target;
                        }
                    }
                    currentAgent.isStopped = false;
                    currentAgent.destination = target.CurrentGameObject.transform.position;
                }
            }
        }
        public virtual void StopFollow()
        {
            currentAgent.isStopped = true;
        }
        private void OnDestroy()
        {
            UnitManager.RemoveFollower(this);
        }

    }
}