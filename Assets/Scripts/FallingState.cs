using UnityEngine;

public class FallingState : State
{
    public FallingState(CharacterMovement character, StateMachine stateMachine) : base(character, stateMachine) { }

    public override void Enter() { }

    public override void HandleInput()
    {
        if (character.isGrounded)
        {
            stateMachine.ChangeState(character.idleState);
        }
        if (Input.GetKeyDown(KeyCode.Space))
        {
            stateMachine.ChangeState(character.jumpingState);
        }
        if (Input.GetAxisRaw("Horizontal") != 0 )
        {
            if (character.isTouchingWall)
            {
                stateMachine.ChangeState(character.climbingState);
            } else
            {
                stateMachine.ChangeState(character.movingState);
            }
        }
        
    }

    public override void LogicUpdate() { }

    public override void PhysicsUpdate() {
        if (character.inventory.Climbing && character.isTouchingWall)
        {
            if (Input.GetKey(KeyCode.S))
            {
                character.rb.velocity = new Vector2(character.rb.velocity.x, -character.fallSpeed);
            } else
            {
                character.rb.velocity = new Vector2(character.rb.velocity.x, -character.wallFallSpeed);
            }
        }

    }

    public override void Exit() { }
}
