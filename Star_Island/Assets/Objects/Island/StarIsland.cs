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

    [Header("Detect Ground")]
    [SerializeField]
    private Transform groundDetectPos;
    [SerializeField]
    private float groundDetectRadius = 5f;
    [SerializeField]
    private LayerMask obstacleLayer;

    private Quaternion rotateTarget;
    private bool rotating = false;
    private float curRotateTime = 0;

    private void Update()
    {
        Rotate();
        Move();
        DetectObstacle();
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
    private void DetectObstacle()
    {
        Collider2D obstacle = Physics2D.OverlapCircle(groundDetectPos.position, groundDetectRadius, obstacleLayer);

        if (obstacle == null)
            return;

        GameManager.Inst.EndGame();
    }

    public void SetRotation(float localX, float maxX, int starPower)
    {
        curRotateTime = 0;
        rotateTarget = transform.rotation;
        float rotation = (startMaxRotation + (starPower * rotationPerStarPower)) * Mathf.Lerp(0, maxX, Mathf.Abs(localX)) * Mathf.Sign(localX);
        rotateTarget = Quaternion.Euler(0, 0, rotateTarget.eulerAngles.z - rotation);

        rotating = true;
    }
}
