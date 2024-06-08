using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputDetection : MonoBehaviour
{
    private CharacterMovement _characterMovement;

    private void Start()
    {
        _characterMovement = GetComponent<CharacterMovement>();
    }

    void Update()
    {
        if (Input.GetKey(InputManager.Instance.walkRight))
        {
           // _characterMovement.MoveRight();
        }

        if (Input.GetKey(InputManager.Instance.walksLeft))
        {
            //_characterMovement.MoveLeft();
        }

        if (Input.GetKeyDown(InputManager.Instance.jump))
        {
            //_characterMovement.Jump();
        }

        if (Input.GetKeyUp(InputManager.Instance.jump))
        {
            //_characterMovement.StopJump();
        }
    }
}
