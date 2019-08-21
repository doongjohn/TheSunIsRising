using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StarIsland : MonoBehaviour
{
    [Header("Movement")]
    [SerializeField]
    private float moveSpeed;

    [Header("Rotation")]
    [SerializeField]
    private float startMaxRotation = 10f;
    [SerializeField]
    private float rotationPerStarPower = 1f;
    [SerializeField]
    private float rotateSpeed = 3f;

    private Quaternion rotateTarget;
    private bool rotating = false;
    private float curRotateTime = 0;

    private void Update()
    {
        Rotate();
        Move();
    }

    private void Move()
    {
        transform.Translate(Vector3.up * moveSpeed * Time.deltaTime, Space.Self);
    }
    private void Rotate()
    {
        if (rotating)
        {
            if (curRotateTime >= 1)
                rotating = false;

            transform.rotation = Quaternion.Slerp(transform.rotation, rotateTarget, curRotateTime);
            curRotateTime += rotateSpeed * Time.deltaTime;
        }
    }

    public void SetRotation(float localX, float maxX)
    {
        curRotateTime = 0;
        rotateTarget = transform.rotation;
        float rotation = (startMaxRotation * Mathf.Lerp(0, maxX, Mathf.Abs(localX)) * Mathf.Sign(localX));
        rotateTarget = Quaternion.Euler(0, 0, rotateTarget.eulerAngles.z - rotation);

        rotating = true;
    }
}
