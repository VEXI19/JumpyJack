using UnityEngine;


public class IdleState : State
{
    public IdleState(CharacterMovement character, StateMachine stateMachine) : base(character, stateMachine) { }

    public override void Enter() { }

    public override void HandleInput()
    {
        if (Input.GetButtonDown("Jump"))
        {
            stateMachine.ChangeState(character.jumpingState);
        }
        else if (character.isTouchingWall && ((Input.GetAxis("Horizontal") < 0 && !character.isWallRight) || (Input.GetAxis("Horizontal") > 0 && character.isWallRight)))
        {
            stateMachine.ChangeState(character.climbingState);
        }
        else if (Input.GetAxis("Horizontal") != 0)
        {
            stateMachine.ChangeState(character.movingState);
        }

        if (character.rb.velocity.y < 0)
        {
            stateMachine.ChangeState(character.fallingState);
        }

    }

    public override void LogicUpdate() { }

    public override void PhysicsUpdate() { }

    public override void Exit() { }
}
