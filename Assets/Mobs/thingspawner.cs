using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class thingspawner : MonoBehaviour
{
    public GameObject thing;
    private GameObject[] things;
    public bool isRandom = false;
    private bool isrunning = true;

    void Start()
    {
        things = new GameObject[2];

        StartCoroutine(waiter());
    }

    IEnumerator waiter()
    {
        while (isrunning)
        {
            isrunning = false;

            float waitTime = isRandom ? Random.value * 2 + 2 : 3; // if random range = [2, 4], else 3
            yield return new WaitForSeconds(waitTime);

            for (int i = 0; i < 20; i++)
            {
                transform.position += new Vector3(0, 0.1f, 0);

                yield return new WaitForSeconds(0.02f);
            }

            for (int i = 0; i < 2; i++)
            {
                if (things[i] != null)
                {
                    Destroy(things[i]);
                }

                things[i] = Instantiate(thing, position: this.transform.position + new Vector3(0,0.5f,-0.1f), this.transform.rotation);

                var x = Random.value * 4 - 2;

                things[i].GetComponent<Rigidbody2D>().velocity = new Vector2(x, 6f);

                yield return new WaitForSeconds(0.2f);
            }

            for (int i = 0; i < 20; i++)
            {
                transform.position -= new Vector3(0, 0.1f, 0);

                yield return new WaitForSeconds(0.02f);
            }
        }
    }

    private void Update()
    {
        isrunning = true;
    }
}
