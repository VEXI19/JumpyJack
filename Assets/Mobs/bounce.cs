using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bounce : MonoBehaviour
{
    private Rigidbody2D rb;
    private Animator animator;
    Vector2 velocity = new Vector2(0, 8f);
    // Start is called before the first frame update
    void Start()
    {
        rb = this.gameObject.GetComponent<Rigidbody2D>();
        animator = rb.GetComponent<Animator>();
        velocity = new Vector2(0, 8f);
    }

    public void OnCollisionEnter2D(Collision2D target)
    {
        if (target.gameObject.CompareTag("Ground"))
        {
            animator.Play("jumpy_squish");
            rb.velocity = velocity;
        }
    }
}
