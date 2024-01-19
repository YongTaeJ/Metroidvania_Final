using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerGroundState : PlayerBaseState
{
    protected int xinput;

    private bool JumpInput;
    public PlayerGroundState(PlayerTemp player, PlayerStateMachine machine, PlayerData playerData, string animBoolName) : base(player, machine, playerData, animBoolName)
    {
    }

    public override void DoChecks()
    {
        base.DoChecks();
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

        xinput = _player.Controller.NormInputX;
        JumpInput = _player.Controller.JumpInput;

        if (JumpInput)
        {
            _player.Controller.UseJumpInput();
            _machine.ChangeState(_player.JumpState);
        }
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }
}
