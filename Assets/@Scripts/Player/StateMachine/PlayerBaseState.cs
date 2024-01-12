using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBaseState : IState
{
    protected PlayerStateMachine _machine;
    protected PlayerInputController _controller;
    protected Transform _cameraTransform;

    public PlayerBaseState(PlayerStateMachine machine)
    {
        _machine = machine;
        _controller = machine.Controller;
        _cameraTransform = machine.CameraTransform;
    }

    public virtual void OnStateEnter() { }

    public virtual void OnStateExit() { }

    public virtual void OnStateStay() { }
}
