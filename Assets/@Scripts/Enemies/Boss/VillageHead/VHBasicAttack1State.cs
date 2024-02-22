using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VHBasicAttack1State : BossAttackState
{
    public VHBasicAttack1State(EnemyStateMachine stateMachine) : base(stateMachine)
    {
        BossPatternType = BossPatternType.Melee;
    }

    public override void OnStateEnter()
    {
        base.OnStateEnter();
        _animator.SetTrigger(AnimatorHash.Attack);
        _animator.SetInteger(AnimatorHash.PatternNumber, 0);
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
        _isAttackEnded = true;
    }
}
