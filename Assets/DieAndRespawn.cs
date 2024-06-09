using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DieAndRespawn : MonoBehaviour
{
    public GameObject doublejump;
    public GameObject climbing;
    public GameObject umbrella;
    public GameObject hologram;

    private Animator animator;
    private Rigidbody2D rb;
    private Checkpoints chk;
    private InventoryManager inventory;
    // Start is called before the first frame update
    void Start()
    {
        animator = this.GetComponent<Animator>();
        rb = this.GetComponent<Rigidbody2D>();
        chk = this.GetComponent<Checkpoints>();
        inventory = this.GetComponent<InventoryManager>();
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

        transform.position = chk.savedCheckpoint;
        inventory.Restore();

        if (!inventory.DoubleJump)
        {
            doublejump.GetComponent<SpriteRenderer>().enabled = true;
        }
        if (!inventory.Climbing)
        {
            climbing.GetComponent<SpriteRenderer>().enabled = true;
        }
        if (!inventory.Umbrella)
        {
            umbrella.GetComponent<SpriteRenderer>().enabled = true;
        }
        if (!inventory.Hologram)
        {
            hologram.GetComponent<SpriteRenderer>().enabled = true;
        }

        if (inventory._hologramUsed)
        {
            Destroy(inventory.holo);
            inventory._hologramUsed = false;
        }

        rb.constraints = RigidbodyConstraints2D.FreezeRotation;
        animator.Play("idle");
    }
}
