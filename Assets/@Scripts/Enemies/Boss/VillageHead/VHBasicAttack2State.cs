using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VHBasicAttack2State : BossAttackState
{
    private int _endCount;
    public VHBasicAttack2State(EnemyStateMachine stateMachine) : base(stateMachine)
    {
        BossPatternTypes = new List<BossPatternType>(){BossPatternType.Melee};

    }

    public override void OnStateEnter()
    {
        base.OnStateEnter();
        _endCount = 0;
        _animator.SetTrigger(AnimatorHash.Attack);
        _animator.SetInteger(AnimatorHash.PatternNumber, 1);
    }

    public override void OnStateExit()
    {
        base.OnStateExit();
        _animator.SetInteger(AnimatorHash.PatternNumber, -1);
    }

    public override void OnStateStay()
    {
        if(_isAttackEnded == true)
        {
            (_stateMachine as BossStateMachine).PatternTransition();
        }
    }

    protected override void OnAttack()
    {
        SFX.Instance.PlayOneShot("EnemyAttackSound");
        _attackCollider.enabled = true;
    }

    protected override void OnAttackEnd()
    {
        _attackCollider.enabled = false;

        if(++_endCount >= 2)
        {
            _isAttackEnded = true;
        }
    }
}
