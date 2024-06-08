using UnityEngine;


public class MovingState : State
{
    public MovingState(CharacterMovement character, StateMachine stateMachine) : base(character, stateMachine) { }

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
        else if (Input.GetAxis("Horizontal") == 0)
        {
            stateMachine.ChangeState(character.idleState);
        }
        
        
        //else if (character.isTouchingWall && character.inventory.Climbing)
        //{
        //    stateMachine.ChangeState(character.climbingState);
        //}
    }

    public override void LogicUpdate() { }

    public override void PhysicsUpdate()
    {
        float moveInput = Input.GetAxis("Horizontal");
        character.rb.velocity = new Vector2(moveInput * character.moveSpeed, character.rb.velocity.y);
    }

    public override void Exit() { }
}
