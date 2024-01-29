using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerWalkState : PlayerGroundState
{
    public PlayerWalkState(PlayerTemp player, PlayerStateMachine machine, PlayerData playerData, string animBoolName) : base(player, machine, playerData, animBoolName)
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

        _player.CheckIfShouldFlip(xinput);

        _player.SetVelocityx(_playerData.movementVelocity * xinput);

        if(xinput == 0f)
        {
            _machine.ChangeState(_player.IdleState);
        }
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }
}
