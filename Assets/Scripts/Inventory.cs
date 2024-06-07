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

    public Vector3 posDoublejump = Vector3.zero;
    public Vector3 posClimbing = Vector3.zero;
    public Vector3 posUmbrella = Vector3.zero;
    public Vector3 posHologram = Vector3.zero;

    public void ActivateDoubleJump(Vector3 pos) { _doubleJump = true; posDoublejump = pos; }

    public void ActivateClimbing(Vector3 pos) { _climbing = true; posClimbing = pos; }

    public void ActivateUmbrella(Vector3 pos) { _umbrella = true; posUmbrella = pos; }

    public void ActivateHologram(Vector3 pos) { _hologram = true; posHologram = pos; }

    public bool DoubleJump { get => _doubleJump; set => _doubleJump = value; }

    public bool Climbing { get => _climbing; set => _climbing = value; }

    public bool Umbrella { get => _umbrella; set => _umbrella = value; }

    public bool Hologram { get => _hologram; set => _hologram = value; }
    public bool HologramUsed { get => _hologramUsed; }

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

       

    }

}
 