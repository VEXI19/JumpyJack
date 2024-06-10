using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Checkpoints : MonoBehaviour
{
    [HideInInspector] public Vector3 savedCheckpoint;

    private Rigidbody2D rb;
    private InventoryManager savedInventory;
    private InventoryManager inventory;
    // Start is called before the first frame update
    void Start()
    {
        savedCheckpoint = new Vector3(2.52f, -4.89f, 0f); // starting position
        transform.position = savedCheckpoint;
        rb = GetComponent<Rigidbody2D>();
        inventory = GetComponent<InventoryManager>();
        inventory.Save();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(InputManager.Instance.teleportToCheckpoint))
        {
            rb.velocity = new Vector2(0, 0);
            transform.position = savedCheckpoint;

            if (inventory._hologramUsed)
            {
                Destroy(inventory.holo);
                inventory._hologramUsed = false;
            }
        }        
    }

    public void SaveNewCheckpoint(Vector3 newCheckpoint)
    {
        savedCheckpoint = new Vector3(newCheckpoint.x, newCheckpoint.y, newCheckpoint.z);
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Checkpoint") && Input.GetKey(InputManager.Instance.saveCheckpoint))
        {
            Vector3 newPosition = collision.gameObject.transform.position;
            newPosition.y++;
            SaveNewCheckpoint(newPosition);
            inventory.Save();

            collision.gameObject.GetComponent<Animator>().Play("checkpoint_active");
            foreach (var chk in GameObject.FindGameObjectsWithTag("Checkpoint"))
            {
                if (collision.gameObject.GetInstanceID() == chk.GetInstanceID()) continue;
                chk.GetComponent<Animator>().Play("checkpoint");
            }
        }
    }
}
