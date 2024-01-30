using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossIdleState : BossBaseState
{
    private float _idleTime;
    public BossIdleState(EnemyStateMachine stateMachine) : base(stateMachine)
    {
        BossPatternType = BossPatternType.None;
    }

    public override void OnStateEnter()
    {
        _idleTime = Random.Range(1f, 1.5f);
        _animator.SetTrigger(AnimatorHash.Prepare);
        _animator.SetBool(AnimatorHash.Idle, true);
    }

    public override void OnStateExit()
    {
        _animator.SetBool(AnimatorHash.Idle, false);
    }

    public override void OnStateStay()
    {
        _idleTime -= Time.deltaTime;
        if(_idleTime <= 0)
        {
            (_stateMachine as BossStateMachine).PatternTransition();
        }
    }
}