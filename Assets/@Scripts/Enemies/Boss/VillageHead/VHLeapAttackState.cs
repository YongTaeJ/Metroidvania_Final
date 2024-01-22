using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VHLeapAttackState : BossAttackState
{
    private int _startCount;
    private int _endCount; 
    private Collider2D _throwCollider;
    public VHLeapAttackState(EnemyStateMachine stateMachine) : base(stateMachine)
    {
        BossPatternType = BossPatternType.Random;
        _throwCollider = stateMachine.transform.Find("ThrowAttack").GetComponent<Collider2D>();
    }

    public override void OnStateEnter()
    {
        _animator.SetInteger(AnimatorHash.PatternNumber, 3);
        _startCount = 0;
        _endCount = 0;
    }

    public override void OnStateExit()
    {
        _animator.SetInteger(AnimatorHash.PatternNumber, -1);
    }

    public override void OnStateStay()
    {
        throw new System.NotImplementedException();
    }

    protected override void OnAttack()
    {
        _throwCollider.enabled = true;
    }

    protected override void OnAttackEnd()
    {
        _throwCollider.enabled = false;
    }
}
