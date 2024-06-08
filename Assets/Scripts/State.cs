using UnityEngine.TextCore.Text;

public abstract class State
{
    protected CharacterMovement character;
    protected StateMachine stateMachine;

    protected State(CharacterMovement character, StateMachine stateMachine)
    {
        this.character = character;
        this.stateMachine = stateMachine;
    }

    public abstract void Enter();
    public abstract void HandleInput();
    public abstract void LogicUpdate();
    public abstract void PhysicsUpdate();
    public abstract void Exit();
}