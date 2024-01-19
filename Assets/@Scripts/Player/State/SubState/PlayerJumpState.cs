using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerJumpState : PlayerAbilityState
{
    public PlayerJumpState(PlayerTemp player, PlayerStateMachine machine, PlayerData playerData, string animBoolName) : base(player, machine, playerData, animBoolName)
    {
    }

    public override void Enter()
    {
        base.Enter();

        _player.SetVelocityY(_playerData.jumpVelocity);
        isAbilityDone = true;
    }
}
