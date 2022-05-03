using UnityEngine;

namespace Labirint
{
    public class Enemy : Follower
    {
        private void OnTriggerEnter(Collider other)
        {
            GamerController gamer = other.GetComponent<GamerController>();
            if (gamer != null)
            {
                Debug.Log("Enemy touch");
                GameManager.GameOver();
            }
        }
    }
}