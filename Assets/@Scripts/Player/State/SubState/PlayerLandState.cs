using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerLandState : PlayerGroundState
{
    public PlayerLandState(PlayerTemp player, PlayerStateMachine machine, PlayerData playerData, string animBoolName) : base(player, machine, playerData, animBoolName)
    {
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();

        if(xinput != 0)
        {
            _machine.ChangeState(_player.MoveState);
        }
        else if (isAnimationFinished)
        {
            _machine.ChangeState(_player.IdleState);
        }
    }
}
