using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tracker : MonoBehaviour
{

    public float posx;
    public float posy;
    public float distance(float x, float y) => Mathf.Sqrt((x - posx) * (x - posx) + (y - posy) * (y - posy));
    private InventoryManager playerinv;

    // Start is called before the first frame update
    void Start()
    {
        playerinv = this.gameObject.GetComponent<InventoryManager>();
    }

    // Update is called once per frame
    void Update()
    {
        posx = playerinv._hologramUsed ? playerinv.holo.transform.position.x : this.gameObject.transform.position.x;
        posy = playerinv._hologramUsed ? playerinv.holo.transform.position.y : this.gameObject.transform.position.y;
    }
}
