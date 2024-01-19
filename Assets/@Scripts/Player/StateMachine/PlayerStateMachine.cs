using System.Collections.Generic;
using UnityEngine;

public class PlayerStateMachine
{
    public PlayerBaseState PlayerBaseState { get; private set; }

    public void Initialize(PlayerBaseState startingState)
    {
        PlayerBaseState = startingState;
        PlayerBaseState.Enter();
    }

    public void ChangeState(PlayerBaseState newstate)
    {
        PlayerBaseState.Exit();
        PlayerBaseState = newstate;
        PlayerBaseState.Enter();
    }
}