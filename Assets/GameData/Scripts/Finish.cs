using UnityEngine;

namespace Labirint
{
    public class Finish : MonoBehaviour
    {
        private void OnTriggerEnter(Collider other)
        {
            GamerController gamer = other.GetComponent<GamerController>();
            if(gamer != null)
            {
                GameManager.GameFinish();
            }
        }
    }
}