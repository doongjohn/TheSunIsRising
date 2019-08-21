using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    // Singleton
    public static Player Inst { get; private set; }

    #region Var - Inspector
    [Header("Player")]
    [SerializeField]
    private GameObject playerGO;

    [Header("Island")]
    [SerializeField]
    private Transform islandStar;

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
    private float fallSpeed = 1f;

    [Header("Collect Star")]
    [SerializeField]
    private float startCollectRadius;
    [SerializeField]
    private float startScale;
    [SerializeField]
    private float maxScale;

    [Header("Effects")]
    [SerializeField]
    private GameObject slamEffect;
    #endregion

    #region Var - Components
    StarIsland starIslandMovement;
    #endregion

    #region Var - Movement
    private Vector2 velocity = Vector2.zero;
    private int walkDir = 0;
    private float curGravity = 0;
    private bool canGroundSlam = false;
    #endregion

    #region Var - Star Power
    private int starPower = 0;
    private int maxStarPower = 10;
    private float sizePerStar;
    private float collectRadius;
    #endregion

    #region Var - Properties
    public GameObject PlayerGO => playerGO;
    public int StarPower => starPower;
    public Vector2 Velocity => velocity;
    #endregion

    #region Method - Unity
    private void Awake()
    {
        // Initialize Singleton
        Inst = this;

        // Get Component
        starIslandMovement = GetComponent<StarIsland>();

        // Initialize Value
        maxLocalX = Mathf.Abs(maxLocalX);
        sizePerStar = (maxScale - startScale) / maxStarPower;
        collectRadius = startCollectRadius;
    }
    private void Update()
    {
        Walk();
        Jump();
        Gravity();
        GroundSlam();
        CollectStar();

        ApplyVelocity();
        ClampPosition();
    }
    #endregion

    #region Method - Movement
    private void ApplyVelocity()
    {
        playerGO.transform.Translate(velocity * Time.deltaTime, Space.Self);
    }
    private void ApplyForce(Vector2 force)
    {
        velocity += force * Time.deltaTime;
    }
    private void ClampPosition()
    {
        Vector3 localPos = playerGO.transform.localPosition;
        localPos.x = Mathf.Clamp(localPos.x, -maxLocalX, maxLocalX);
        localPos.y = Mathf.Max(localPos.y, minLocalY);

        // Apply Clamped Position
        playerGO.transform.localPosition = localPos;
    }
    private bool IsGrounded(bool usePadding = false)
    {
        if (!usePadding)
            return playerGO.transform.localPosition.y <= minLocalY + groundedPadding ? true : false;
        else
            return playerGO.transform.localPosition.y == minLocalY ? true : false;
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
        if (IsGrounded() && !canGroundSlam /*&& Input.GetKeyDown(KeySettings.Jump)*/)
        {
            playerGO.transform.localPosition = new Vector3(playerGO.transform.localPosition.x, minLocalY);
            velocity.y = GetJumpVelocity(jumpHeight, jumpTime);
            canGroundSlam = true;
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
        if (canGroundSlam && velocity.y < 0 && IsGrounded())
        {
            canGroundSlam = false;
            starIslandMovement.SetRotation(playerGO.transform.localPosition.x, maxLocalX, StarPower);
        }
    }
    #endregion

    #region Method - Gravity
    private void Gravity()
    {
        if (playerGO.transform.localPosition.y > minLocalY)
        {
            // Gravity On Jump
            if (velocity.y > 0)
            {
                curGravity = GetJumpGravity(jumpHeight, jumpTime);
            }
            // Gravity On Fall
            else
            {
                velocity.y = -fallSpeed;
            }
        }
        else if (velocity.y < 0)
        {
            curGravity = 0;
        }

        ApplyForce(new Vector2(0, curGravity));
    }
    #endregion

    #region Method - Collect Star
    private void CollectStar()
    {
        if (IsGrounded())
            return;

        Collider2D[] star = Physics2D.OverlapCircleAll(playerGO.transform.position, collectRadius);

        if (star == null)
            return;

        for (int i = 0; i < star.Length; i++)
        {
            if (star[i].CompareTag("Star"))
                ChangeStarPower(star[i].GetComponent<StarItem>().Consume(islandStar));
        }
    }
    public void ChangeStarPower(int amount)
    {
        if (amount < 0)
            starPower = Mathf.Max(starPower + amount, 0);
        else
            starPower = Mathf.Min(starPower + amount, maxStarPower);

        playerGO.transform.localScale = new Vector3(startScale + (starPower * sizePerStar), startScale + (starPower * sizePerStar), 0);
        collectRadius = startCollectRadius + (starPower * sizePerStar);
    }
    #endregion
}
