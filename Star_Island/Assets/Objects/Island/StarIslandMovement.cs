using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StarIslandMovement : MonoBehaviour
{
    [Header("Movement")]
    [SerializeField]
    private float moveSpeed;

    [Header("Rotation")]
    [SerializeField]
    private float maxRotation = 30f;
    [SerializeField]
    private float rotateTime = 0.4f;

    private Quaternion rotateTarget;
    private bool rotating = false;
    private float curRotateTime = 0;

    public float MoveSpeed 
    {
        get {
            return moveSpeed + AGameMananger.inst.DashPower;
        } set => moveSpeed = value; 
    }

    private void Update()
    {
        Rotate();
        Move();
    }

    private void Move()
    {
        transform.Translate(Vector3.up * MoveSpeed * Time.deltaTime, Space.Self);
    }
    private void Rotate()
    {
        if (rotating)
        {
            if (curRotateTime >= 1)
                rotating = false;

            transform.rotation = Quaternion.Slerp(transform.rotation, rotateTarget, curRotateTime);
            curRotateTime += rotateTime * Time.deltaTime;
        }
    }
    public void SetRotation(float localX, float maxX)
    {
        curRotateTime = 0;
        rotateTarget = transform.rotation;
        float rotation = maxRotation * Mathf.Lerp(0, maxX, Mathf.Abs(localX)) * Mathf.Sign(localX);
        rotateTarget = Quaternion.Euler(0, 0, rotateTarget.eulerAngles.z - rotation);

        rotating = true;
    }
}
