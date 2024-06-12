using UnityEngine;

public class FallingState : State
{
    private InputManager inputManager;
    public FallingState(CharacterMovement character, StateMachine stateMachine) : base(character, stateMachine) { }

    public override void Enter()
    {
        if (!GameObject.FindGameObjectWithTag("Player").GetComponent<DieAndRespawn>().Locked)
            GameObject.FindGameObjectWithTag("Player").GetComponent<Animator>().Play("jump_down");
    }

    public override void HandleInput()
    {
        if (character.isGrounded)
        {
            stateMachine.ChangeState(character.idleState);
        }
        if (Input.GetKeyDown(InputManager.Instance.jump))
        {
            stateMachine.ChangeState(character.jumpingState);
        }
        if (character.inventory.Climbing && ((Input.GetKey(InputManager.Instance.left) && character.isTouchingWallLeft) || (Input.GetKey(InputManager.Instance.right) && character.isTouchingWallRight)))
        {
           stateMachine.ChangeState(character.climbingState);
        }
        if ((InputManager.Instance.GetAxis("Horizontal") < 0 && !character.isTouchingWallLeft) || (InputManager.Instance.GetAxis("Horizontal") > 0 && !character.isTouchingWallRight))
        {
            stateMachine.ChangeState(character.movingState);
        }
        
    }

    public override void LogicUpdate() { }

    public override void PhysicsUpdate() {
        if (character.inventory.Climbing && character.isTouchingWall)
        {
            if (Input.GetKey(InputManager.Instance.down))
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
