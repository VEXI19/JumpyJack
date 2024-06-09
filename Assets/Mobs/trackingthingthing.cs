using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.Dependencies.Sqlite;
using UnityEngine;

public class trackingthingthing : MonoBehaviour
{
    private Vector2 where;
    private float speed = 0.6f;
    private Tracker tracker;
    private Rigidbody2D rb;
    // Start is called before the first frame update
    void Start()
    {
        tracker = GameObject.FindGameObjectWithTag("Player").GetComponent<Tracker>();
        rb = this.gameObject.GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if (tracker.distance(transform.position.x, transform.position.y) < 9f)
        {
            var x = (tracker.posx - transform.position.x);
            var y = (tracker.posy - transform.position.y);
            var norm = Mathf.Sqrt(x * x + y * y);

            where = new Vector2(x / norm * speed, y / norm * speed);
            rb.velocity *= 0.9f;
            rb.velocity += where;
        }
    }

    public void OnCollisionEnter2D(Collision2D target)
    {
        if (target.gameObject.CompareTag("Ground") || target.gameObject.CompareTag("Player"))
        {
            Destroy(this.gameObject, 0.02f);
        }
    }
}
