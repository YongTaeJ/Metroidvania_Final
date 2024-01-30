using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KSBreathAttackState : BossAttackState
{
    private int _currentBreathCount;
    private int _breathCount;
    private GameObject _breathArea;
    public KSBreathAttackState(EnemyStateMachine stateMachine) : base(stateMachine)
    {
        BossPatternType = BossPatternType.Melee;
        _breathArea = _attackPivot.Find("BreathArea").gameObject;
    }

    public override void OnStateEnter()
    {
        base.OnStateEnter();
        _animator.SetTrigger(AnimatorHash.Attack);
        _animator.SetInteger(AnimatorHash.PatternNumber, 0);
        _currentBreathCount = 0;
        _breathCount = Random.Range(2,5);
    }

    public override void OnStateExit()
    {
        base.OnStateExit();
        _breathArea.SetActive(false);
        _animator.SetInteger(AnimatorHash.PatternNumber, -1);
    }

    public override void OnStateStay()
    {
        if(_isAttackEnded)
        {
            (_stateMachine as BossStateMachine).PatternTransition();
            return;
        }

        if(_currentBreathCount != 0)
        {
            GetDirection();
            _objectFlip.Flip(_direction);
        }
    }

    protected override void OnAttack()
    {
        _breathArea.SetActive(true);
    }

    protected override void OnAttackEnd()
    {
        _currentBreathCount++;

        if(_currentBreathCount == _breathCount)
        {
            _isAttackEnded = true;
        }
    }
}
