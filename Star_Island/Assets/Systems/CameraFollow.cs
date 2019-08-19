using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [SerializeField]
    private Transform target;
    [SerializeField]
    private float offsetX;


    void LateUpdate()
    {
        Vector3 pos = target.position + (target.up * offsetX);
        pos.z = transform.position.z;

        transform.position = pos;
    }
}
