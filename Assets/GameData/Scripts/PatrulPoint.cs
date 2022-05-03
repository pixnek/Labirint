using UnityEngine;

namespace Labirint
{
    public class PatrulPoint : MonoBehaviour, ITargetFollower
    {
        [SerializeField] private Color gizmosColor;
        [SerializeField] private float radiusSphere;

        public GameObject CurrentGameObject => gameObject;

        private void OnDrawGizmos()
        {
            Gizmos.color = gizmosColor;
            Gizmos.DrawSphere(transform.position, radiusSphere);
        }
    }
}