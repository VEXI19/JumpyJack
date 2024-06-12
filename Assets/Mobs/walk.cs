
using UnityEngine;

public class walk : MonoBehaviour
{
    private Vector2 velocity;
    private Rigidbody2D rb;
    private float isright;
    private float speed = 3f;
    // Start is called before the first frame update
    void Start()
    {
        isright = transform.localScale.x > 0 ? 1 : -1;
        rb = this.GetComponent<Rigidbody2D>();
        velocity = new Vector3(speed * isright, 0, 0);
    }

    // Update is called once per frame
    void Update()
    {
        rb.velocity = velocity;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isright *= -1;
            velocity = new Vector3(speed * isright, 0, 0);

            Vector3 localScale = transform.localScale;
            localScale.x *= -1f;
            transform.localScale = localScale;
        }
    }
}
