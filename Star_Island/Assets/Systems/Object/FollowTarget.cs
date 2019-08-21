using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowTarget : MonoBehaviour
{
    [SerializeField]
    private Transform target;
    [SerializeField]
    private bool followPos = true;
    [SerializeField]
    private bool followRot = true;

    private void LateUpdate()
    {
        if (followPos)
            transform.position = target.position;

        if (followRot)
            transform.rotation = target.rotation;
    }
}
