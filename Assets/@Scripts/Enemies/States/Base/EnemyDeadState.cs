using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDeadState : EnemyBaseState
{
    public EnemyDeadState(EnemyStateMachine stateMachine) : base(stateMachine)
    {
    }

    public override void OnStateEnter()
    {
        _animator.SetTrigger(AnimatorHash.Dead);
    }

    public override void OnStateExit()
    {
    }

    public override void OnStateStay()
    {
    }

    // TODO. 애니메이션 끝나면 아이템 드랍, 게임 오브젝트 삭제
}
