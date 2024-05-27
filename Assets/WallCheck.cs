using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallCheck : MonoBehaviour
{
    public GameObject player;
    private PlayerMovement playerMovement;
    private InventoryManager inventoryManager;

    void Start()
    {
        playerMovement = player.GetComponent<PlayerMovement>();
        inventoryManager = player.GetComponent<InventoryManager>();
    }


    // Update is called once per frame
    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground") && inventoryManager.Climbing)
        {
            playerMovement.ResetJumpCount();
        }
        
    }
}
