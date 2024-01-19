using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAirState : PlayerBaseState
{
    private int xInput;
    private bool isGrounded;

    public PlayerAirState(PlayerTemp player, PlayerStateMachine machine, PlayerData playerData, string animBoolName) : base(player, machine, playerData, animBoolName)
    {
    }

    public override void DoChecks()
    {
        base.DoChecks();

        isGrounded = _player.CheckGround();
    }

    public override void Enter()
    {
        base.Enter();
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();

        xInput = _player.Controller.NormInputX;

        if(isGrounded && _player.CurrentVelocity.y < 0.01f)
        {
            _machine.ChangeState(_player.LandState);
        }
        else
        {
            _player.CheckIfShouldFlip(xInput);
            _player.SetVelocityx(_playerData.movementVelocity * xInput);

            _player.Anim.SetFloat("yVelocity", _player.CurrentVelocity.y);
            _player.Anim.SetFloat("xVelocity", Mathf.Abs(_player.CurrentVelocity.x));

        }
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }
}
