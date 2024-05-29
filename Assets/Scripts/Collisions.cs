using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class Collisions : MonoBehaviour
{
    private void OnCollisionStay(UnityEngine.Collision target)
    {
        
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        Checkpoints checkpoints = this.gameObject.GetComponent<Checkpoints>();

        if (collision.gameObject.CompareTag("Checkpoint") && Input.GetKey(KeyCode.S))
        {
            Vector3 newPosition = collision.gameObject.transform.position;
            newPosition.y++;
            checkpoints.SaveNewCheckpoint(newPosition);
        }
    }
    private void OnCollisionEnter2D(UnityEngine.Collision2D target)
    {
        InventoryManager inventoryManager = this.gameObject.GetComponent<InventoryManager>();
        PlayerMovement playerMovement = this.gameObject.GetComponent<PlayerMovement>();
        
        if (target.gameObject.CompareTag("DoubleJumpPowerUp"))
        {
            inventoryManager.ActivateDoubleJump();
        }

        if (target.gameObject.CompareTag("ClimbingPowerUp"))
        {
            inventoryManager.ActivateClimbing();
        }

        if (target.gameObject.CompareTag("UmbrellaPowerUp"))
        {
            inventoryManager.ActivateUmbrella();
        }

        if (target.gameObject.CompareTag("HologramPowerUp"))
        {
            inventoryManager.ActivateHologram();
        }


        
    }

}
