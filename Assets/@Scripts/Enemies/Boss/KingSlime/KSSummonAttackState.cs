using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KSSummonAttackState : BossAttackState
{
    private KSSpecialPattern _pattern;
    public KSSummonAttackState(EnemyStateMachine stateMachine) : base(stateMachine)
    {
        BossPatternType = BossPatternType.Special;
        _pattern = GameObject.Find("KSSpecialPattern").GetComponent<KSSpecialPattern>();
    }

    public override void OnStateEnter()
    {
        base.OnStateEnter();
        _animator.SetTrigger(AnimatorHash.Attack);
        _animator.SetInteger(AnimatorHash.PatternNumber, 2);
    }

    public override void OnStateExit()
    {
        base.OnStateExit();
        _animator.SetInteger(AnimatorHash.PatternNumber, -1);
    }

    public override void OnStateStay()
    {
        if(_isAttackEnded)
        {
            (_stateMachine as BossStateMachine).PatternTransition();
        }
    }

    protected override void OnAttack()
    {
        _pattern.SummonEnemies();
    }

    protected override void OnAttackEnd()
    {
        _isAttackEnded = true;
    }
}
