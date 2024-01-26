using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FixedEnemyStateMachine : EnemyStateMachine
{
    public override void Initialize()
    {
        // TODO => 구조 고민. Speed를 0으로 조정하면 그대로 사용이 가능한지?
        base.Initialize();
        StateDictionary = new()
        {
            { EnemyStateType.Idle, new EnemyIdleState(this) },
            { EnemyStateType.Wander, new EnemyIdleState(this) },
            { EnemyStateType.Chase, new EnemyRangedAttackState(this)},
            { EnemyStateType.Hurt, new EnemyHurtState(this)},
            { EnemyStateType.Dead, new EnemyDeadState(this)}
        };
    }
}
