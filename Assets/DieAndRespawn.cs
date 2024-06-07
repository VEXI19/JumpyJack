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

        transform.position = chk.SavedCheckpoint;

        if(inventory.DoubleJump != chk.savedDoublejump)
        {
            inventory.DoubleJump = chk.savedDoublejump;
            Instantiate(doublejump, position: inventory.posDoublejump, this.transform.rotation);
        }
        if (inventory.Climbing != chk.savedClimbing)
        {
            inventory.Climbing = chk.savedClimbing;
            Instantiate(climbing, position: inventory.posClimbing, this.transform.rotation);
        }
        if (inventory.Umbrella != chk.savedUmbrella)
        {
            inventory.Umbrella = chk.savedUmbrella;
            Instantiate(umbrella, position: inventory.posUmbrella, this.transform.rotation);
        }
        if (inventory.Hologram != chk.savedHologram)
        {
            inventory.Hologram = chk.savedHologram;
            Instantiate(hologram, position: inventory.posHologram, this.transform.rotation);
        }

        rb.constraints = RigidbodyConstraints2D.FreezeRotation;
        animator.Play("idle");
    }
}
