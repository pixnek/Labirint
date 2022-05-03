using UnityEngine;

public class DoorController : MonoBehaviour
{
    [SerializeField] private MeshRenderer meshRenderer;
    public void DoorStateSwitch()
    {
        gameObject.SetActive(!gameObject.activeSelf);
    }
    public void SetColor(Color inColor)
    {
        meshRenderer.material.color = inColor;
    }
}
