using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkySunRising : MonoBehaviour
{
    [Header("Move")]
    [SerializeField]
    private float moveSpeed;

    [Header("StarIsland")]
    [SerializeField]
    private Transform starIsland;


    private void Update()
    {
        Move();
        ClampPosition();
    }
    private void Move()
    {
        transform.Translate(Vector3.right * moveSpeed * Time.deltaTime, Space.World);
    }
    private void ClampPosition()
    {
        Vector3 pos = transform.position;
        pos.y = starIsland.position.y;

        transform.position = pos;
    }
}
