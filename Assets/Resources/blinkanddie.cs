using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class blinkanddie : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        GameObject.Find("powerupsfx").GetComponent<AudioSource>().Play();
        Destroy(this.gameObject, 0.15f);
    }
}
