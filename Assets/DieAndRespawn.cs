using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DieAndRespawn : MonoBehaviour
{
    private Animator animator;
    private Rigidbody2D rb;
    // Start is called before the first frame update
    void Start()
    {
        animator = this.GetComponent<Animator>();
        rb = this.GetComponent<Rigidbody2D>();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Toxic"))
        {
            animator.Play("death");
            rb.constraints = RigidbodyConstraints2D.FreezePosition | RigidbodyConstraints2D.FreezeRotation;

            StartCoroutine(waiter());
        }
    }

    IEnumerator waiter()
    {
        yield return new WaitForSeconds(1f);

        transform.position = this.gameObject.GetComponent<Checkpoints>().SavedCheckpoint;
        rb.constraints = RigidbodyConstraints2D.FreezeRotation;
        animator.Play("idle");
    }
}
