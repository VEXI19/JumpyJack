using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class balling : MonoBehaviour
{
    private Vector2 velocity;
    public float isright = 1;
    private Rigidbody2D rb;
    // Start is called before the first frame update
    void Start()
    {
        velocity = new Vector2(2f * isright, 0);
        rb = this.gameObject.GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        rb.velocity = velocity;
    }
}
