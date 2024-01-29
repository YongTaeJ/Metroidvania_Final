using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.RestService;
using UnityEngine;

public class PlayerBaseState
{
    protected PlayerStateMachine _machine;
    protected PlayerTemp _player;
    protected PlayerData _playerData;

    protected bool isAnimationFinished;

    protected float _startTime;

    private string _animBoolName;

    public PlayerBaseState(PlayerTemp player, PlayerStateMachine machine, PlayerData playerData, string animBoolName)
    {
        this._player = player;
        this._machine = machine;
        this._playerData = playerData;
        this._animBoolName = animBoolName;
    }

    public virtual void Enter()
    {
        DoChecks();
        _player.Anim.SetBool(_animBoolName, true);
        _startTime = Time.time;
        isAnimationFinished = false;
    }

    public virtual void Exit() 
    {
        _player.Anim.SetBool(_animBoolName, false);
    }

    public virtual void LogicUpdate() { }

    public virtual void PhysicsUpdate() 
    {
        DoChecks();
    }

    public virtual void DoChecks() 
    {

    }

    public virtual void AnimationTrigger() { }

    public virtual void AnimationFinishTrigger() => isAnimationFinished = true;
}
