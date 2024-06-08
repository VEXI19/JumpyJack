using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroudCheck : MonoBehaviour
{
    public GameObject player;
    private PlayerMovement playerMovement;

    void Start()
    {
        playerMovement = player.GetComponent<PlayerMovement>();
    }

    // Update is called once per frame
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            playerMovement.ResetJumpCount();
        }
    }
}
