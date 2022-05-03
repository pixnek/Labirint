using UnityEngine;

namespace Labirint
{
    public class DoorButtonController : MonoBehaviour
    {
        [SerializeField] private Color color;
        [SerializeField] private MeshRenderer currentMeshRenderer;
        [SerializeField] private DoorController currentDoor;

        private void Start()
        {
            currentMeshRenderer.material.color = color;
            currentDoor.SetColor(color);
        }
        private void OnTriggerEnter(Collider other)
        {
            GamerController gamer = other.GetComponent<GamerController>();
            if(gamer != null)
            {
                currentDoor.DoorStateSwitch();
            }
        }
    }
}