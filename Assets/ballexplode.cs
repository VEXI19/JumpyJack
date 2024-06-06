using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ballexplode : MonoBehaviour
{
    private Animator animator;
    // Start is called before the first frame update
    void Start()
    {
        animator = this.GetComponent<Animator>();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            Destroy(this.gameObject,0.15f);
            animator.Play("ball_explode");
        }
    }
}
