using UnityEngine;

public class JumpingState : State
{
    public JumpingState(CharacterMovement character, StateMachine stateMachine) : base(character, stateMachine) { }

    public override void Enter()
    {
        if (!GameObject.FindGameObjectWithTag("Player").GetComponent<DieAndRespawn>().Locked)
            GameObject.FindGameObjectWithTag("Player").GetComponent<Animator>().Play("jump_up");

        if (character.isTouchingWall && ((InputManager.Instance.GetAxisRaw("Horizontal") < 0 && character.isWallRight) || (InputManager.Instance.GetAxisRaw("Horizontal") > 0 && !character.isWallRight)))
        {
            character.rb.velocity = new Vector2(InputManager.Instance.GetAxisRaw("Horizontal") * character.moveSpeed, character.jumpForce);

            character.jumpCount++;
        } else if (character.jumpCount < character.maxJumpCount && character.groundCheckTimer <= 0)
        {
            character.rb.velocity = new Vector2(character.rb.velocity.x, character.jumpForce);

            character.jumpCount++;
            character.groundCheckTimer = character.groundCheckCooldown;
        }
        stateMachine.ChangeState(character.idleState);
    }

    public override void HandleInput() { }

    public override void LogicUpdate() { }

    public override void PhysicsUpdate() {
    }

    public override void Exit() { }
}
