using UnityEngine;

public class StateMachine
{
    public State CurrentState { get; private set; }

    public void Initialize(State startingState)
    {
        CurrentState = startingState;
        startingState.Enter();
    }

    public void ChangeState(State newState)
    {
        Debug.Log(newState.ToString());
        CurrentState.Exit();
        CurrentState = newState;
        newState.Enter();
    }
}
