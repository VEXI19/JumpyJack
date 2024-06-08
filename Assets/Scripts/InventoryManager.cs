using System.Collections;
using System.Collections.Generic;
using System.Net.Http.Headers;
using System.Runtime.CompilerServices;
using Unity.VisualScripting;
using UnityEditor.SceneManagement;
using UnityEngine;

public class InventoryManager : MonoBehaviour
{
    private bool _doubleJump = false;
    private bool _climbing = false;
    private bool _umbrella = false;
    private bool _hologram = false;
    private bool _hologramUsed = false;
    private GameObject holo;

    public void ActivateDoubleJump() { _doubleJump = true; }

    public void ActivateClimbing() { _climbing = true; }

    public void ActivateUmbrella() { _umbrella = true; }

    public void ActivateHologram() { _hologram = true; }

    public bool DoubleJump { get => _doubleJump; }

    public bool Climbing { get => _climbing; }

    public bool Umbrella { get => _umbrella; }

    public bool Hologram { get => _hologram; }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("DoubleJumpPowerUp"))
        {
            _doubleJump = true;
        }
        if (collision.gameObject.CompareTag("ClimbingPowerUp"))
        {
            _climbing = true;
        }
        if (collision.gameObject.CompareTag("UmbrellaPowerUp"))
        {
            _umbrella = true;
        }
        if (collision.gameObject.CompareTag("HologramPowerUp"))
        {
            _hologram = true;
        }
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.G))
        {
            Debug.Log(!_hologram);
        }


        if (Input.GetKeyDown(KeyCode.G) && _hologram)
        {
            if (!_hologramUsed)
            {
                holo = Instantiate(this.gameObject, position: this.transform.position, this.transform.rotation);
                _hologramUsed = true;
                
                MonoBehaviour[] scripts = holo.GetComponents<MonoBehaviour>();
                foreach (MonoBehaviour script in scripts)
                {
                    script.enabled = false;
                }

                BoxCollider2D[] boxColliders = holo.GetComponents<BoxCollider2D>();
                foreach (BoxCollider2D bc in boxColliders)
                {
                    bc.excludeLayers = LayerMask.GetMask("Default");
                }

                for (var i = holo.transform.childCount - 1; i >= 0; i--)
                {
                    Object.Destroy(holo.transform.GetChild(i).gameObject);
                }
            }

            else
            {
                Destroy(holo);
                _hologramUsed = false;
            }


        }

        if (_hologramUsed)
        {
            float distance = Vector3.Distance(holo.transform.position, transform.position);
            if (distance > 13f)
            {
                Destroy(holo);
                _hologramUsed = false;
            }
        }

       

    }

}
 