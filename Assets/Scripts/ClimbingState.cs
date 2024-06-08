

using Unity.Mathematics;
using UnityEngine;

public class ClimbingState : State
{
    public ClimbingState(CharacterMovement character, StateMachine stateMachine) : base(character, stateMachine) { }

    public override void Enter() { 
        if (!character.inventory.Climbing)
        {
            stateMachine.ChangeState(character.idleState);
        }
    }

    public override void HandleInput()
    {
        if (Input.GetAxisRaw("Horizontal") == 0)
        {
            stateMachine.ChangeState(character.fallingState);
        }
        else if (Input.GetButtonDown("Jump"))
        {
            stateMachine.ChangeState(character.jumpingState);
        }
        else if (!character.isTouchingWall)
        {
            stateMachine.ChangeState(character.idleState);
        } else if (character.isTouchingWall && ((Input.GetAxis("Horizontal") < 0 && character.isWallRight) || (Input.GetAxis("Horizontal") > 0 && !character.isWallRight)))
        {
            stateMachine.ChangeState(character.movingState);
        }

    }

    public override void LogicUpdate() { }

    public override void PhysicsUpdate()
    {
        float horizontalInput = Input.GetAxis("Horizontal");
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
