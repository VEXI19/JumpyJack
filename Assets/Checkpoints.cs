using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Checkpoints : MonoBehaviour
{
    private Vector3 _savedCheckpoint;
    // Start is called before the first frame update
    void Start()
    {
        _savedCheckpoint = new Vector3(2.52f, -4.89f, 0f); // starting position
        transform.position = _savedCheckpoint;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.T))
        {
            transform.position = _savedCheckpoint;
        }        
    }

    public void SaveNewCheckpoint(Vector3 newCheckpoint)
    {
        _savedCheckpoint = new Vector3(newCheckpoint.x, newCheckpoint.y, newCheckpoint.z);
    }
}
