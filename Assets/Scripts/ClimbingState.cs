
using UnityEngine;

public class ClimbingState : State
{
    public ClimbingState(CharacterMovement character, StateMachine stateMachine) : base(character, stateMachine) { }

    public override void Enter() { 
        if (!character.inventory.Climbing)
        {
            stateMachine.ChangeState(character.idleState);
        }

        if (!GameObject.FindGameObjectWithTag("Player").GetComponent<DieAndRespawn>().Locked)
            GameObject.FindGameObjectWithTag("Player").GetComponent<Animator>().Play("climb");
    }

    public override void HandleInput()
    {
        if (InputManager.Instance.GetAxisRaw("Horizontal") == 0)
        {
            stateMachine.ChangeState(character.fallingState);
        }
        else if (Input.GetKeyDown(InputManager.Instance.jump))
        {
           stateMachine.ChangeState(character.jumpingState);
        } 
        else if (character.isTouchingWall && ((Input.GetKey(InputManager.Instance.left) && !character.isTouchingWallLeft) || (Input.GetKey(InputManager.Instance.right) && !character.isTouchingWallRight)))
        {
            stateMachine.ChangeState(character.movingState);
        } else if (!character.isTouchingWall && InputManager.Instance.GetAxisRaw("Horizontal") != 0)
        {
            stateMachine.ChangeState(character.movingState);
        }
        else if (!character.isTouchingWall)
        {
            stateMachine.ChangeState(character.idleState);
        }

    }

    public override void LogicUpdate() { }

    public override void PhysicsUpdate()
    {
        float horizontalInput = InputManager.Instance.GetAxis("Horizontal");
        if (character.rb.velocity.y <= character.climbSpeed)
        {
            if(character.isWallRight)
            {
                character.rb.velocity = new Vector2(0, horizontalInput * character.climbSpeed);
            } else
            {
                character.rb.velocity = new Vector2(0, -horizontalInput * character.climbSpeed);

            }
        }
    }

    public override void Exit() { }
}
