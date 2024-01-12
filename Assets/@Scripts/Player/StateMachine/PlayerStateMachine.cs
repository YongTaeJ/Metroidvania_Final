using System.Collections.Generic;
using UnityEngine;

public class PlayerStateMachine : StateMachine<PlayerBaseState>
{
    public PlayerInputController Controller { get; private set; }
    public Transform CameraTransform { get; private set; }

    public enum PlayerState
    {
        Idle,
        Dead,
    }

    private Dictionary<PlayerState, PlayerBaseState> _stateDict;

    public Dictionary<PlayerState, PlayerBaseState> StateDictionary => _stateDict;

    private void Awake()
    {
        Initialize();
    }

    public void Initialize()
    {
        Controller = GetComponent<PlayerInputController>();
        CameraTransform = Camera.main.transform;
        _stateDict = new()
        {
            { PlayerState.Idle, new PlayerIdleState(this) },
            { PlayerState.Dead, new PlayerDeadState(this) },
        };
        StateTransition(_stateDict[PlayerState.Idle]);
    }
}