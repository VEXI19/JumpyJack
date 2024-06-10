using UnityEngine;


public class MovingState : State
{
    public MovingState(CharacterMovement character, StateMachine stateMachine) : base(character, stateMachine) { }

    public override void Enter()
    {
        if (!GameObject.FindGameObjectWithTag("Player").GetComponent<DieAndRespawn>().Locked)
            GameObject.FindGameObjectWithTag("Player").GetComponent<Animator>().Play("run");
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
        else if (InputManager.Instance.GetAxis("Horizontal") == 0)
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
        float moveInput = InputManager.Instance.GetAxis("Horizontal");
        character.rb.velocity = new Vector2(moveInput * character.moveSpeed, character.rb.velocity.y);
    }

    public override void Exit() { }
}
