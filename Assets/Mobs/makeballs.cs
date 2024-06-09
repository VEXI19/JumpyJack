using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class makeballs : MonoBehaviour
{
    public GameObject ball;
    public float isright;
    private bool isrunning = true;
    // Start is called before the first frame update
    void Start()
    {
        isright = transform.localScale.x > 0 ? 1 : -1;
        StartCoroutine(waiter());
    }

    IEnumerator waiter()
    {
        while (isrunning)
        {
            isrunning = false;

            yield return new WaitForSeconds(3.5f);

            GameObject bbll = Instantiate(ball, position: this.transform.position + new Vector3(0.5f*isright,0,-0.1f), this.transform.rotation);
            bbll.GetComponent<balling>().isright = isright;

            yield return new WaitForSeconds(0.15f);

        }
    }

    private void Update()
    {
        isrunning = true;
    }
}
