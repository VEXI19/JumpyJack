using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class proximity : MonoBehaviour
{
    private Tracker tracker;
    private bool active = false;
    // Start is called before the first frame update
    void Start()
    {
        tracker = GameObject.FindGameObjectWithTag("Player").GetComponent<Tracker>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!active && tracker.distance(this.transform.position.x, this.transform.position.y + 1) < 4f)
        {
            active = true;
            StartCoroutine(up());
        }
        if (active && tracker.distance(this.transform.position.x, this.transform.position.y + 1) > 4f)
        {
            active = false;
            StartCoroutine(down());
        }
    }


    IEnumerator up()
    {
        for (int i = 0; i < 14; i++)
        {
            transform.position += new Vector3(0, 0.125f, 0);

            yield return new WaitForSeconds(0.02f);
        }
    }

    IEnumerator down()
    {
        for (int i = 0; i < 14; i++)
        {
            transform.position -= new Vector3(0, 0.125f, 0);

            yield return new WaitForSeconds(0.02f);
        }
    }
}