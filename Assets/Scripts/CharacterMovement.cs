using Unity.IO.LowLevel.Unsafe;
using UnityEngine;
using UnityEngine.TextCore.Text;

[RequireComponent(typeof(Rigidbody2D), typeof(Collider2D))]
public class CharacterMovement : MonoBehaviour
{
    public float moveSpeed = 5f;
    public float jumpForce = 10f;
    public float climbSpeed = 5f;
    public float wallFallSpeed = 3.0f;
    public float fallSpeed = 10f;

    public LayerMask groundLayer;
    private float rayLength = 0.1f;
    public Transform groundCheckRight, groundCheckLeft, shoulderCheckLeft, shoulderCheckRight;
    private Vector2[] wallChecksRight = new Vector2[12];
    private Vector2[] wallChecksLeft = new Vector2[12];

    [HideInInspector] public bool isTouchingWallLeft;
    [HideInInspector] public bool isTouchingWallRight;

    private bool isFacingRight = true;

    [HideInInspector] private DieAndRespawn dnr;
    [HideInInspector] public Rigidbody2D rb;
    [HideInInspector] public Collider2D coll;
    [HideInInspector] public InventoryManager inventory;
    [HideInInspector] public bool isGrounded = false;
    [HideInInspector] public bool isTouchingWall = false;
    [HideInInspector] public uint jumpCount = 0;
    [HideInInspector] public uint maxJumpCount = 1;
    [HideInInspector] public bool isWallRight = false;
    [HideInInspector] public float groundCheckCooldown = 0.1f;
    [HideInInspector] public float groundCheckTimer;

    public StateMachine stateMachine;
    public IdleState idleState;
    public MovingState movingState;
    public JumpingState jumpingState;
    public FallingState fallingState;
    public ClimbingState climbingState;

    

    public bool IsFacingRight { get => isFacingRight; }

    void Start()
    {
        dnr = GetComponent<DieAndRespawn>();
        rb = GetComponent<Rigidbody2D>();
        coll = GetComponent<Collider2D>();
        inventory = GetComponent<InventoryManager>();

        stateMachine = new StateMachine();

        idleState = new IdleState(this, stateMachine);
        movingState = new MovingState(this, stateMachine);
        jumpingState = new JumpingState(this, stateMachine);
        fallingState = new FallingState(this, stateMachine);
        climbingState = new ClimbingState(this, stateMachine);

        stateMachine.Initialize(idleState);
    }

    void Update()
    {
        stateMachine.CurrentState.HandleInput();
        stateMachine.CurrentState.LogicUpdate();
        maxJumpCount = inventory.DoubleJump ? (uint)2 : (uint)1;
        if (!dnr.Locked)
            Flip();

        for (var i = 0; i < wallChecksRight.Length; i++)
        {
            if (IsFacingRight)
            {
                wallChecksRight[i] = new Vector2(transform.position.x + 0.31f, transform.position.y + (i / 10f - 0.6f));
            } else
            {
                wallChecksRight[i] = new Vector2(transform.position.x + 0.373f, transform.position.y + (i / 10f - 0.6f));
            }
        }

        for (var i = 0; i < wallChecksLeft.Length; i++)
        {
            if (IsFacingRight)
            {
                wallChecksLeft[i] = new Vector2(transform.position.x - 0.373f, transform.position.y + (i / 10f - 0.6f));
            } else
            {
                wallChecksLeft[i] = new Vector2(transform.position.x - 0.31f, transform.position.y + (i / 10f - 0.6f));
            }
        }
    }

    void FixedUpdate()
    {
        CheckSurroundings();

        stateMachine.CurrentState.PhysicsUpdate();

        if (Input.GetKeyUp(InputManager.Instance.jump))
        {
            rb.velocity = new Vector2(rb.velocity.x, rb.velocity.y * 0.5f);
        }
        if (inventory.Umbrella && rb.velocity.y < 0 && Input.GetKey(InputManager.Instance.jump))
        {
            rb.velocity = new Vector2(rb.velocity.x, -2f);
        }
    }

    void Flip()
    {
        float horizontal = InputManager.Instance.GetAxisRaw("Horizontal");
        if (isFacingRight && horizontal < 0f || !isFacingRight && horizontal > 0f)
        {
            isFacingRight = !isFacingRight;
            Vector2 localScale = transform.localScale;
            localScale.x *= -1f;
            transform.localScale = localScale;
        }
    }


        void CheckSurroundings()
        {
            // Check if grounded on left or right
            bool isGroundedLeft = Physics2D.Raycast(groundCheckLeft.position, Vector2.down, rayLength, groundLayer);
            bool isGroundedRight = Physics2D.Raycast(groundCheckRight.position, Vector2.down, rayLength, groundLayer);

            // Check if touching shoulder on left or right
            bool isTouchingShoulderLeft = Physics2D.Raycast(shoulderCheckLeft.position, isFacingRight ? Vector2.left : Vector2.right, rayLength, groundLayer);
            bool isTouchingShoulderRight = Physics2D.Raycast(shoulderCheckRight.position, isFacingRight ? Vector2.right : Vector2.left, rayLength, groundLayer);

            bool isTouchingWallLeftTemp = false;
            bool isTouchingWallRightTemp = false;

            // Check if touching wall on right
            for (int i = 0; i < wallChecksRight.Length; i++)
            {
                bool rayCast = Physics2D.Raycast(wallChecksRight[i], Vector2.right, rayLength, groundLayer);
                isTouchingWallRightTemp = isTouchingWallRightTemp || rayCast;
            }

            // Check if touching wall on left
            foreach (Vector2 wallCheck in wallChecksLeft)
            {
                bool rayCast = Physics2D.Raycast(wallCheck, Vector2.left, rayLength, groundLayer);
                isTouchingWallLeftTemp = isTouchingWallLeftTemp || rayCast;
            }
            bool isShoulderTouching;
            // Update grounded and shoulder touching states based on timer
            if (groundCheckTimer <= 0)
            {
                isGrounded = isGroundedLeft || isGroundedRight;
                isShoulderTouching = isTouchingShoulderRight || isTouchingShoulderLeft;
            }
            else
            {
                isGrounded = false;
                isShoulderTouching = false;
                groundCheckTimer -= Time.deltaTime;
            }

            // Update wall touching states
            isTouchingWallLeft = isTouchingWallLeftTemp;
            isTouchingWallRight = isTouchingWallRightTemp;
            isTouchingWall = isTouchingWallLeft || isTouchingWallRight;
            isWallRight = isTouchingWallRight;

            // Reset jump count if grounded or touching wall
            if (isGrounded)
            {
                this.jumpCount = 0;
            }

            if (isTouchingWall && inventory.Climbing)
            {
                this.jumpCount = 0;
            }

            if (isShoulderTouching)
            {
                this.jumpCount = 0;
            }

        //Debug.Log($"Grounded Left: {isGroundedLeft}, Grounded Right: {isGroundedRight}");
        //Debug.Log($"Touching Shoulder Left: {isTouchingShoulderLeft}, Touching Shoulder Right: {isTouchingShoulderRight}");
        //Debug.Log($"Touching Wall Left: {isTouchingWallLeftTemp}, Touching Wall Right: {isTouchingWallRightTemp}");
    }


    private void OnDrawGizmosSelected()
    {
        //Gizmos.color = Color.red;

        //foreach (Vector2 wallCheck in wallChecksRight)
        //{
        //    Gizmos.DrawLine(wallCheck, wallCheck + Vector2.right * rayLength);
        //}
        
        //foreach (Vector2 wallCheck in wallChecksLeft)
        //{
        //    Gizmos.DrawLine(wallCheck, wallCheck + Vector2.left * rayLength);
        //}

        Gizmos.color = Color.red;
        Gizmos.DrawLine(groundCheckLeft.position, groundCheckLeft.position + Vector3.down * rayLength);
        Gizmos.DrawLine(groundCheckRight.position, groundCheckRight.position + Vector3.down * rayLength);

        Gizmos.DrawLine(shoulderCheckLeft.position, shoulderCheckLeft.position + (isFacingRight ? Vector3.left : Vector3.right) * rayLength);
        Gizmos.DrawLine(shoulderCheckRight.position, shoulderCheckRight.position + (isFacingRight ? Vector3.right : Vector3.left) * rayLength);

        foreach (Vector2 wallCheck in wallChecksRight)
        {
            Gizmos.DrawLine(wallCheck, wallCheck + Vector2.right * rayLength);
        }

        foreach (Vector2 wallCheck in wallChecksLeft)
        {
            Gizmos.DrawLine(wallCheck, wallCheck + Vector2.left * rayLength);
        }


        //Gizmos.DrawLine(groundCheckLeft.position, groundCheckLeft.position + Vector3.down * rayLength);
        //Gizmos.DrawLine(groundCheckRight.position, groundCheckRight.position + Vector3.down * rayLength);
        //Gizmos.DrawLine(wallCheckLeftUp.position, wallCheckLeftUp.position + (isFacingRight ? Vector3.left : Vector3.right)  * rayLength);
        //Gizmos.DrawLine(wallCheckLeftDown.position, wallCheckLeftDown.position + (isFacingRight ? Vector3.left : Vector3.right) * rayLength);
        //Gizmos.DrawLine(wallCheckRightUp.position, wallCheckRightUp.position + (isFacingRight ? Vector3.right : Vector3.left) * rayLength);
        //Gizmos.DrawLine(wallCheckRightDown.position, wallCheckRightDown.position + (isFacingRight ? Vector3.right : Vector3.left) * rayLength);
        //Gizmos.DrawLine(shoulderCheckLeft.position, shoulderCheckLeft.position + (isFacingRight ? Vector3.left : Vector3.right) * rayLength);
        //Gizmos.DrawLine(shoulderCheckRight.position, shoulderCheckRight.position + (isFacingRight ? Vector3.right : Vector3.left) * rayLength);

        //Gizmos.DrawLine(wallCheckLeft.position, wallCheckLeft.position + Vector3.left * rayLength);
        // Gizmos.DrawLine(wallCheckRight.position, wallCheckRight.position + Vector3.right * rayLength);
    }
}
