using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class fly : MonoBehaviour
{
    private Vector2 where;
    private float speed = 1.5f;
    private float scale = 3f;
    private Tracker tracker;
    private Rigidbody2D rb;
    // Start is called before the first frame update
    void Start()
    {
        tracker = GameObject.FindGameObjectWithTag("Player").GetComponent<Tracker>();
        rb = this.gameObject.GetComponent<Rigidbody2D>();
        StartCoroutine(waiter());
    }

    // Update is called once per frame
    void Update()
    {
        if(tracker.distance(transform.position.x, transform.position.y) < 5f)
        {
            var x = (tracker.posx - transform.position.x);
            var y = (tracker.posy - transform.position.y);
            var norm = Mathf.Sqrt(x * x + y * y);

            where = new Vector2(x / norm * speed * scale, y / norm * speed * scale);
            rb.velocity = where;
        }
    }

    IEnumerator waiter()
    {
        while (true)
        {
            var x = Random.value;
            var y = Random.value;
            var norm = Mathf.Sqrt(x * x + y * y);

            where = new Vector2 (x / norm * speed, y / norm * speed);
            rb.velocity = where;

            yield return new WaitForSeconds(3f);
        }
    }

    public void OnCollisionEnter2D(Collision2D target)
    {
        if (target.gameObject.CompareTag("Ground"))
        {
            if(target.GetContact(0).normal.x == 0)
            {
                where.y *= -1;
            }
            else if (target.GetContact(0).normal.y == 0)
            {
                where.x *= -1;
            }
            else
            {
                where *= -1;
            }

            rb.velocity = where;
        }
    }
}
