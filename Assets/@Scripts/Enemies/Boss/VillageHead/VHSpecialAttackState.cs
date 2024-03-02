using System.Collections;
using System.Collections.Generic;
using System.Dynamic;
using UnityEngine;

public class VHSpecialAttackState : BossAttackState
{
    private VHSpeicalPattern _pattern;
    private int _patternCount;

    public VHSpecialAttackState(EnemyStateMachine stateMachine) : base(stateMachine)
    {
        
        BossPatternTypes = new List<BossPatternType>(){BossPatternType.Special};

        _pattern = GameObject.Find("VillageHeadSpecialPattern").GetComponent<VHSpeicalPattern>();
    }

    public override void OnStateEnter()
    {
        base.OnStateEnter();
        TimerManager.Instance.StartTimer(1.1f , () => SFX.Instance.PlayOneShot("VillageHeadTauntSound", 0.5f) );
        _animator.SetTrigger(AnimatorHash.Attack);
        _animator.SetInteger(AnimatorHash.PatternNumber, 5);
        _patternCount = 0;
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
            _pattern.CreateRock();
            TimerManager.Instance.StartTimer(0.3f, () => _pattern.CreateRock());
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
