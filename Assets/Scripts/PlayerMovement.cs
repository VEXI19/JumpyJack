using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private float horizontal;
    private float vertical;
    private bool isFacingRight = true;
    public int jumpCount = 0;
    public int jumpCount = 0;

    [SerializeField] private float speed = 8f;
    [SerializeField] private float jumpingPower = 16f;

    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private Transform groundCheckLeft;
    [SerializeField] private Transform groundCheckRight;
    [SerializeField] private Transform groundCheckLeft;
    [SerializeField] private Transform groundCheckRight;
    [SerializeField] private LayerMask groundLayer;
    [SerializeField] private InventoryManager inventoryManager;

    [SerializeField] private InventoryManager inventoryManager;

    void Update()
    {
        horizontal = Input.GetAxisRaw("Horizontal");
        vertical = Input.GetAxisRaw("Vertical");

        int maxJumpCount = inventoryManager.DoubleJump ? 2 : 1;

        int maxJumpCount = inventoryManager.DoubleJump ? 2 : 1;

        if (Input.GetButtonDown("Jump") && jumpCount < maxJumpCount)
        if (Input.GetButtonDown("Jump") && jumpCount < maxJumpCount)
        {
            jumpCount++;
            jumpCount++;
            rb.velocity = new Vector2(rb.velocity.x, jumpingPower);
        }

        if (Input.GetButtonUp("Jump") && rb.velocity.y > 0f)
        {
            rb.velocity = new Vector2(rb.velocity.x, rb.velocity.y * 0.5f);
        }

        Flip();
    }

    public void Climb()
    {
        rb.velocity = new Vector2(rb.velocity.x, speed);
    }

    public void ResetJumpCount() 
    {
        jumpCount = 0;
    } 
    }

    public void Climb()
    {
        rb.velocity = new Vector2(rb.velocity.x, horizontal * speed);
    }

    public void WallFall()
    {
        rb.velocity = new Vector2(rb.velocity.x, rb.velocity.y);
    }

    public void WallFallClimb()
    {
        rb.velocity = new Vector2(rb.velocity.x, rb.velocity.y * 0.9f);
    }

    public void ResetJumpCount() 
    {
        jumpCount = 0;
    } 

    private void FixedUpdate()
    {
        rb.velocity = new Vector2(horizontal * speed, rb.velocity.y);
    }

    private void Flip()
    {
        if (isFacingRight && horizontal < 0f || !isFacingRight && horizontal > 0f)
        {
            isFacingRight = !isFacingRight;
            Vector3 localScale = transform.localScale;
            localScale.x *= -1f;
            transform.localScale = localScale;
        }
    }

}
