using UnityEngine;


public class IdleState : State
{
    public IdleState(CharacterMovement character, StateMachine stateMachine) : base(character, stateMachine) { }

    public override void Enter()
    {
        if (!GameObject.FindGameObjectWithTag("Player").GetComponent<DieAndRespawn>().Locked)
            GameObject.FindGameObjectWithTag("Player").GetComponent<Animator>().Play("idle");
    }

    public override void HandleInput()
    {
        if (Input.GetKeyDown(InputManager.Instance.jump))
        {
            stateMachine.ChangeState(character.jumpingState);
        }
        else if (character.isTouchingWall && ((InputManager.Instance.GetAxis("Horizontal") < 0 && !character.isWallRight) || (InputManager.Instance.GetAxis("Horizontal") > 0 && character.isWallRight)))
        {
            stateMachine.ChangeState(character.climbingState);
        }
        else if (InputManager.Instance.GetAxis("Horizontal") != 0)
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
