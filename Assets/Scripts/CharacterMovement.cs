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

    [HideInInspector] public Rigidbody2D rb;
    [HideInInspector] public Collider2D coll;
    [HideInInspector] public InventoryManager inventory;
    [HideInInspector] public bool isGrounded = false;
    [HideInInspector] public bool isTouchingWall = false;
    [HideInInspector] public uint jumpCount = 0;
    [HideInInspector] public uint maxJumpCount = 1;
    [HideInInspector] public bool isWallRight = false;

    public StateMachine stateMachine;
    public IdleState idleState;
    public MovingState movingState;
    public JumpingState jumpingState;
    public FallingState fallingState;
    public ClimbingState climbingState;

    void Start()
    {
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
    }

    void FixedUpdate()
    {
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

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Ground"))
        {
            if (transform.position.y > collision.transform.position.y)
            {
                isGrounded = true;
                this.jumpCount = 0;
            }
        }

        if (collision.gameObject.layer == LayerMask.NameToLayer("Wall"))
        {
            if (transform.position.x > collision.transform.position.x)
            {
                isWallRight = false;
            }
            else
            {
                isWallRight = true;
            }
            isTouchingWall = true;
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Wall"))
        {
            if (inventory.Climbing)
            {
                jumpCount = 0;
            }
        }


    }

    void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Ground"))
        {
            isGrounded = false;
        }

        if (collision.gameObject.layer == LayerMask.NameToLayer("Wall"))
        {
            isTouchingWall = false;
        }
    }
}
