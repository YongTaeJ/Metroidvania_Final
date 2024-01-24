using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VHSpecialAttackState : BossAttackState
{
    private VHSpeicalPattern _pattern;
    private int _patternCount;
    private int _endCount;

    public VHSpecialAttackState(EnemyStateMachine stateMachine) : base(stateMachine)
    {
        // TODO => 개선 필요
        BossPatternType = BossPatternType.Special;
        _pattern = GameObject.Find("VillageHeadSpecialPattern").GetComponent<VHSpeicalPattern>();
    }

    public override void OnStateEnter()
    {
        base.OnStateEnter();
        _animator.SetTrigger(AnimatorHash.Attack);
        _animator.SetInteger(AnimatorHash.PatternNumber, 5);

        _patternCount = 0;
        _endCount = 0;
    }

    public override void OnStateExit()
    {
        base.OnStateExit();
        _animator.SetInteger(AnimatorHash.PatternNumber, -1);
    }

    public override void OnStateStay()
    {
    }

    protected override void OnAttack()
    {
        _patternCount++;

        if(_patternCount <= 10)
        {
            // TODO => 함성 이펙트 혹은 연출효과 부여!
            _pattern.CreateRock();
        }
        else
        {
            _animator.SetInteger(AnimatorHash.PatternNumber, -2);
        }
    }

    protected override void OnAttackEnd()
    {
        (_stateMachine as BossStateMachine).PatternTransition();
    }
}
