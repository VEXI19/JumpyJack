using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collision : MonoBehaviour
{
    public GameObject blinker;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            if (this.gameObject.GetComponent<SpriteRenderer>().enabled) Instantiate(blinker, transform.position + new Vector3(0, 0, 0.2f), transform.rotation);
            this.gameObject.GetComponent<SpriteRenderer>().enabled = false;
        }
    }
}
