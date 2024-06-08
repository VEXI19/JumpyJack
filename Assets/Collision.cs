using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collision : MonoBehaviour
{
    public GameObject blinker;
    public void OnCollisionEnter2D(Collision2D target)
    {
        if (target.gameObject.CompareTag("Player"))
        {
            Destroy(this.gameObject);
            Instantiate(blinker, transform.position, transform.rotation);
        }
    }
}
