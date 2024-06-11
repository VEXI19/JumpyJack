using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Checkpoints : MonoBehaviour
{
    [HideInInspector] public Vector3 savedCheckpoint;
    public GameObject startingCheckpoint;
    public AudioSource checkpointsfx;

    private Rigidbody2D rb;
    private InventoryManager inventory;
    // Start is called before the first frame update
    void Start()
    {
        Vector3 startingPosition = startingCheckpoint.transform.position;
        SaveNewCheckpoint(startingPosition);
        startingCheckpoint.GetComponent<Animator>().Play("checkpoint_active");
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
            checkpointsfx.GetComponent<AudioSource>().Play();

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
            if (!checkpointsfx.GetComponent<AudioSource>().isPlaying)
                checkpointsfx.GetComponent<AudioSource>().Play();

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
