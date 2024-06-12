using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class minelauncher : MonoBehaviour
{
    public GameObject mine;
    public bool isRandom = false;
    private GameObject[] mines;
    private bool isrunning = true;
    private Animator animator;

    void Start()
    {
        mines = new GameObject[5];
        animator = this.GetComponent<Animator>();

        StartCoroutine(waiter());
    }

    IEnumerator waiter()
    {
        while(isrunning)
        {
            isrunning = false;

            float waitTime = isRandom ? Random.value * 2 + 2 : 3; // if random range = [2, 4], else 3
            yield return new WaitForSeconds(waitTime);

            this.animator.Play("mine_start");

            yield return new WaitForSeconds(0.1f);

            for (int i = 0; i < 5; i++)
            {
                if (mines[i] != null)
                {
                    Destroy(mines[i]);
                }

                mines[i] = Instantiate(mine, position: this.transform.position + new Vector3(0, 0, 0.1f), this.transform.rotation);

                var x = Random.value * 8 - 4;
                var y = Random.value * 6 + 6;

                mines[i].GetComponent<Rigidbody2D>().velocity = new Vector2(x, y);

                yield return new WaitForSeconds(0.02f);
            }

            yield return new WaitForSeconds(0.1f);
        }
    }

    private void Update()
    {
        isrunning = true;
    }
}
