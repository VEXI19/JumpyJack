using UnityEngine;

public class JumpingState : State
{
    public JumpingState(CharacterMovement character, StateMachine stateMachine) : base(character, stateMachine) { }

    public override void Enter()
    {
        if (character.isTouchingWall && ((Input.GetAxisRaw("Horizontal") < 0 && character.isWallRight) || (Input.GetAxisRaw("Horizontal") > 0 && !character.isWallRight)))
        {
            character.rb.velocity = new Vector2(Input.GetAxisRaw("Horizontal") * character.moveSpeed, character.jumpForce);

            character.jumpCount++;
        } else if (character.jumpCount < character.maxJumpCount)
        {
            character.rb.velocity = new Vector2(character.rb.velocity.x, character.jumpForce);

            character.jumpCount++;
        }
        stateMachine.ChangeState(character.idleState);
    }

    public override void HandleInput() { }

    public override void LogicUpdate() { }

    public override void PhysicsUpdate() { }

    public override void Exit() { }
}
