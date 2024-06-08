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

    private void Update()
    {
        if (inventoryManager.Climbing)
        {
            
        }
    }

    // Update is called once per frame
    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            if (inventoryManager.Climbing)
            {
                if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.A))
                {
                    playerMovement.Climb();
                } else
                {
                    playerMovement.WallFallClimb();
                }
                playerMovement.ResetJumpCount();
            } else
            {
                Debug.Log("fasll");
                playerMovement.WallFall();
            }
        }



        
    }
}
