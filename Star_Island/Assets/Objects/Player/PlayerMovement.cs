using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    #region Var - Inspector
    [Header("Player")]
    [SerializeField]
    private GameObject player;

    [Header("Position")]
    [SerializeField]
    private float maxLocalX;
    [SerializeField]
    private float minLocalY;
    [SerializeField]
    private float groundedPadding = 0.1f;

    [Header("Walk")]
    [SerializeField]
    private float walkSpeed = 2f;

    [Header("Jump")]
    [SerializeField]
    private float jumpHeight = 2f;
    [SerializeField]
    private float jumpTime = 2f;

    [Header("Gravity")]
    [SerializeField]
    private float fallSpeed_Normal = 1f;
    [SerializeField]
    private float fallSpeed_Fast = 1f;
    #endregion

    #region Var - Components
    StarIslandMovement starIslandMovement;
    #endregion

    #region Var - Movement
    private Vector2 velocity = Vector2.zero;
    private int walkDir = 0;
    private float curGravity = 0;
    private bool groundSlaming = false;
    #endregion

    #region Method - Unity
    private void Awake()
    {
        // Get Component
        starIslandMovement = GetComponent<StarIslandMovement>();

        // Initialize Value
        maxLocalX = Mathf.Abs(maxLocalX);
    }
    private void Update()
    {
        Walk();
        Jump();
        Gravity();
        GroundSlam();

        ApplyVelocity();
        ClampPosition();
    }
    #endregion

    #region Method - Movement
    private void ApplyVelocity()
    {
        player.transform.Translate(velocity * Time.deltaTime, Space.Self);
    }
    private void ApplyForce(Vector2 force)
    {
        velocity += force * Time.deltaTime;
    }
    private void ClampPosition()
    {
        Vector3 localPos = player.transform.localPosition;
        localPos.x = Mathf.Clamp(localPos.x, -maxLocalX, maxLocalX);
        localPos.y = Mathf.Max(localPos.y, minLocalY);

        // Apply Clamped Position
        player.transform.localPosition = localPos;
    }
    private bool IsGrounded(bool usePadding = false)
    {
        if (!usePadding)
            return player.transform.localPosition.y <= minLocalY + groundedPadding ? true : false;
        else
            return player.transform.localPosition.y == minLocalY ? true : false;
    }
    #endregion

    #region Method - Walk
    private int GetWalkDir()
    {
        if ((!Input.GetKey(KeySettings.MoveRight) && !Input.GetKey(KeySettings.MoveLeft)) ||
            (Input.GetKey(KeySettings.MoveRight) && Input.GetKey(KeySettings.MoveLeft)))
            return 0;

        return Input.GetKey(KeySettings.MoveRight) ? 1 : Input.GetKey(KeySettings.MoveLeft) ? -1 : 0;
    }
    private void Walk()
    {
        walkDir = GetWalkDir();
        velocity.x = walkSpeed * walkDir;
    }
    #endregion

    #region Method - Jump
    private void Jump()
    {
        if (IsGrounded(true) && Input.GetKeyDown(KeySettings.Jump))
        {
            player.transform.localPosition = new Vector3(player.transform.localPosition.x, minLocalY);
            velocity.y = GetJumpVelocity(jumpHeight, jumpTime);
        }
    }
    private float GetJumpVelocity(float jumpHeight, float jumpTime)
    {
        return Mathf.Abs(GetJumpGravity(jumpHeight, jumpTime)) * jumpTime;
    }
    private float GetJumpGravity(float jumpHeight, float jumpTime)
    {
        return -(2 * jumpHeight) / (jumpTime * jumpTime);
    }
    #endregion

    #region Method - Ground Slam
    private void GroundSlam()
    {
        if (Input.GetKeyDown(KeySettings.GroundSlam))
        {
            groundSlaming = true;

            if (!IsGrounded(true))
            {
                velocity.y = -fallSpeed_Fast;

                // Play Slam Fall Animation
                // Spawn Slam Fall Effect
            }
        }

        if (groundSlaming && IsGrounded(true))
        {
            groundSlaming = false;
            starIslandMovement.SetRotation(player.transform.localPosition.x, maxLocalX);

            // Play Slam Animation
            // Spawn Slam Effect
        }
    }
    #endregion

    #region Method - Gravity
    private void Gravity()
    {
        if (player.transform.localPosition.y > minLocalY)
        {
            // Gravity On Jump
            if (velocity.y > 0)
            {
                curGravity = GetJumpGravity(jumpHeight, jumpTime);
            }
            // Gravity On Fall
            else
            {
                curGravity = -fallSpeed_Normal;
            }
        }
        else if (velocity.y < 0)
        {
            curGravity = 0;
        }

        ApplyForce(new Vector2(0, curGravity));
    }
    #endregion
}
