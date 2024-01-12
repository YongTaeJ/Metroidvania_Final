using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyRangedAttackState : EnemyAttackState
{
    public EnemyRangedAttackState(EnemyStateMachine stateMachine) : base(stateMachine)
    {
        // 공격할 때 탄환을 발사할 수 있도록
    }
}
