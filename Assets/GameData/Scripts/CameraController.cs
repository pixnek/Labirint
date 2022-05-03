using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] private GameObject target;
    [SerializeField] private Vector3 offset;
    private void LateUpdate()
    {
        transform.position = target.transform.TransformPoint(offset);
        transform.LookAt(target.transform);
    }
}
