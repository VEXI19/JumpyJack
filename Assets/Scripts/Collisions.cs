using UnityEngine;

public class Collisions : MonoBehaviour
{
    private InventoryManager _inventoryManager;
    private CharacterMovement _characterMovement;

    private void Start()
    {
        _inventoryManager = GetComponent<InventoryManager>();
        _characterMovement = GetComponent<CharacterMovement>();
    }

    //private void OnTriggerStay2D(Collider2D collision)
    //{
    //    Checkpoints checkpoints = this.gameObject.GetComponent<Checkpoints>();

    //    if (collision.gameObject.CompareTag("Checkpoint") && Input.GetKey(KeyCode.S))
    //    {
    //        Vector3 newPosition = collision.gameObject.transform.position;
    //        newPosition.y++;
    //        checkpoints.SaveNewCheckpoint(newPosition);
    //    }
    //}

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("DoubleJumpPowerUp"))
        {
            _inventoryManager.ActivateDoubleJump();
        }

        if (collision.gameObject.CompareTag("ClimbingPowerUp"))
        {
            _inventoryManager.ActivateClimbing();
        }

        if (collision.gameObject.CompareTag("UmbrellaPowerUp"))
        {
            _inventoryManager.ActivateUmbrella();
        }

        if (collision.gameObject.CompareTag("HologramPowerUp"))
        {
            _inventoryManager.ActivateHologram();
        }
    }


    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            Vector3 hit = collision.contacts[0].normal;
            float angle = Vector3.Angle(hit, Vector3.up);
            if (Mathf.Approximately(angle, 0))
            {
                //Down
                //_characterMovement.ResetJumpCount();
            }
            if (Mathf.Approximately(angle, 180))
            {
                //Upw
            }
            if (Mathf.Approximately(angle, 90))
            {
                // Sides

                Vector3 cross = Vector3.Cross(Vector3.forward, hit);
                if (cross.y > 0)
                {
                    // Left
                }
                else
                {
                    // Right
                }
            }
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            //_characterMovement.SetTouchingLeftWall(false);
            //_characterMovement.SetTouchingRightWall(false);
        }
    }
}
