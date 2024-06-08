using System.Collections;
using System.Collections.Generic;
using System.Net.Http.Headers;
using System.Runtime.CompilerServices;
using Unity.VisualScripting;
using UnityEditor.SceneManagement;
using UnityEngine;


public class InventoryManager : MonoBehaviour
{
    InventoryPowerUps powerUps;
    InventoryPowerUps powerUpsSave;
    private bool _hologramUsed = false;
    private GameObject holo;

    struct InventoryPowerUps
    {
        public bool _doubleJump;
        public bool _climbing;
        public bool _umbrella;
        public bool _hologram;
    }

    private void Start()
    {
        powerUps._climbing = false;
        powerUps._doubleJump = false;
        powerUps._climbing = false;
        powerUps._hologram = false;
    }

    public void Save()
    {
        powerUpsSave = powerUps;
    }

    public void Restore()
    {
        powerUps = powerUpsSave;
    }

    public bool DoubleJump { get => powerUps._doubleJump; }

    public bool Climbing { get => powerUps._climbing; }

    public bool Umbrella { get => powerUps._umbrella; }

    public bool Hologram { get => powerUps._hologram; }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("DoubleJumpPowerUp"))
        {
            powerUps._doubleJump = true;
        }
        if (collision.gameObject.CompareTag("ClimbingPowerUp"))
        {
            powerUps._climbing = true;
        }
        if (collision.gameObject.CompareTag("UmbrellaPowerUp"))
        {
            powerUps._umbrella = true;
        }
        if (collision.gameObject.CompareTag("HologramPowerUp"))
        {
            powerUps._hologram = true;
        }
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.G) && powerUps._hologram)
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
 